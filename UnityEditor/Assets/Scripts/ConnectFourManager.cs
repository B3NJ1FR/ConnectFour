using System;
using System.Linq;
using UnityEngine;
using System.Collections.Generic;

public class ConnectFourManager : MonoBehaviour
{
    public enum Players
    {
        ANY_PLAYER,
        PLAYER_1,
        PLAYER_2,
    }

    private Players currentPlayer;
    private Node.State player1Color;
    private Node.State player2Color;
    int countLeaf = 0;

    Node root;

    private TokenCreator tokenCreator = null;

    uint columnNumber;
    bool isKeyPressed = false;
    bool isCanContinue = false;

    float timer = 0.0f;
    float maxTimer = 2.0f;

    [SerializeField] int maxDepth = 2;

    public Players CurrentPlayer { get => currentPlayer; }
    public Node.State Player1Color { get => player1Color; }
    public Node.State Player2Color { get => player2Color; }

    void Start()
    {
        currentPlayer = Players.PLAYER_1;

        player1Color = Node.State.Red_Token;
        player2Color = Node.State.Yellow_Token;

        tokenCreator = GetComponent<TokenCreator>();

        root = new Node();
        columnNumber = 0;

        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnDisplayHasChanged += UpdateGridToCurrentDisplay;
            GameManager.Instance.OnResetAsked += ResetBoard;

            if (GameManager.Instance.CurrentDifficulty == GameManager.GameDifficulty.MEDIUM
                || GameManager.Instance.CurrentDifficulty == GameManager.GameDifficulty.HARD)
            {
                maxDepth = 4;
            }
        }

    }

    private void OnDisable()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnDisplayHasChanged -= UpdateGridToCurrentDisplay;
            GameManager.Instance.OnResetAsked -= ResetBoard;
        }
    }



    private void Update()
    {
        if (GameManager.Instance != null)
        {
            if (!GameManager.Instance.IsGameIsFinished)
            {
                timer += Time.deltaTime;

                if (timer >= maxTimer)
                {
                    isCanContinue = true;
                }

                if (isCanContinue)
                {
                    if (GameManager.Instance != null)
                    {
                        switch (GameManager.Instance.GameModeChoosen)
                        {
                            case GameManager.GameMode.P_V_P:
                                Inputs();
                                root.isOpponent = (currentPlayer == Players.PLAYER_1) ? false : true;

                                break;

                            case GameManager.GameMode.P_V_AI:
                                if (currentPlayer == Players.PLAYER_1)
                                {
                                    Inputs();
                                }
                                else if (currentPlayer == Players.PLAYER_2)
                                {
                                    PlayAI();
                                    timer = 0.0f;
                                    isCanContinue = false;
                                }
                                root.isOpponent = (currentPlayer == Players.PLAYER_1) ? false : true;

                                break;

                            case GameManager.GameMode.AI_V_AI:
                                PlayAI();
                                root.isOpponent = (currentPlayer == Players.PLAYER_1) ? false : true;

                                timer = 0.0f;
                                isCanContinue = false;

                                break;

                            default:
                                break;
                        }
                    }




                    if (isKeyPressed)
                    {
                        isKeyPressed = false;

                        AddTokenToGrid();
                    }
                }

            }
        }
    }

    void Inputs()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // We want to avoid to add a token into the grid when we clicked on a button
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.name != "Button Switch 2D/3D"
                    && hit.transform.name != "Button Restart"
                    && hit.transform.name != "Button Main Menu")
                {
                    if (ColumnSelection.Instance != null)
                    {
                        columnNumber = (uint)ColumnSelection.Instance.previousColumnNumber;
                        isKeyPressed = true;
                    }
                }
            }
        }
    }


    void AddTokenToGrid()
    {
        if (root.IsColumnFreeForNewToken(columnNumber))
        {
            Vector2 position = Vector2.zero;
            int verticalPosition = root.GetFirstFreePositionInColumn(columnNumber);

            // Add the token to the data's grid
            if (verticalPosition != -1)
            {
                if (currentPlayer == Players.PLAYER_1)
                {
                    root.ChangeStateToBoard(columnNumber, (uint)verticalPosition, player1Color);
                }
                else if (currentPlayer == Players.PLAYER_2)
                {
                    root.ChangeStateToBoard(columnNumber, (uint)verticalPosition, player2Color);
                }
            }

            // Adding visually the token to the grid
            if (currentPlayer == Players.PLAYER_1)
            {
                tokenCreator.CreateToken((int)columnNumber, verticalPosition, player1Color);
            }
            else if (currentPlayer == Players.PLAYER_2)
            {
                tokenCreator.CreateToken((int)columnNumber, verticalPosition, player2Color);
            }


            timer = 0.0f;
            isCanContinue = false;

            // We check if the game is finished
            WinVerification();
            DrawVerification();

            // If the game isn't finished, we change the current player
            if (!GameManager.Instance.IsGameIsFinished)
            {
                SwitchCurrentPlayer();
            }
        }
    }


    void UpdateGridToCurrentDisplay()
    {
        // We remove all falling tokens
        if (FallingTokenList.Instance != null)
        {
            FallingTokenList.Instance.ClearContent();
        }

        for (int y = 0; y < 6; y++)
        {
            for (int x = 0; x < 7; x++)
            {
                if (root.board[y, x] != Node.State.Empty)
                {
                    tokenCreator.CreateToken((int)x, (int)y, root.board[y, x], true);
                }
            }
        }
    }

    void WinVerification()
    {
        if (!GameManager.Instance.IsGameIsFinished)
        {
            if (root.IsAligned() == Node.State.Red_Token)
            {
                if (player1Color == Node.State.Red_Token)
                {
                    Debug.Log("<color=red>PLAYER 1 A WIN</color>");
                }
                else if (player2Color == Node.State.Red_Token)
                {
                    Debug.Log("<color=red>PLAYER 2 A WIN</color>");
                }

                Debug.Log(root.ToString());
                GameManager.Instance.GameHasFinished(GameManager.GameStatus.PLAYER_RED_WIN);
            }
            else if (root.IsAligned() == Node.State.Yellow_Token)
            {
                if (player1Color == Node.State.Yellow_Token)
                {
                    Debug.Log("<color=yellow>PLAYER 1 A WIN</color>");
                }
                else if (player2Color == Node.State.Yellow_Token)
                {
                    Debug.Log("<color=yellow>PLAYER 2 A WIN</color>");
                }

                Debug.Log(root.ToString());
                GameManager.Instance.GameHasFinished(GameManager.GameStatus.PLAYER_YELLOW_WIN);
            }
        }
    }

    void DrawVerification()
    {
        if (!GameManager.Instance.IsGameIsFinished)
        {
            bool isOneCaseStillEmpty = false;

            for (int x = 0; x < 7; x++)
            {
                for (int y = 0; y < 6; y++)
                {
                    if (root.board[y, x] == Node.State.Empty)
                    {
                        isOneCaseStillEmpty = true;
                        break;
                    }
                }
            }

            if (!isOneCaseStillEmpty)
            {
                Debug.Log("<color=red>DRAW !!!!</color>");
                GameManager.Instance.GameHasFinished(GameManager.GameStatus.DRAW);
            }
        }
    }

    void SwitchCurrentPlayer() => currentPlayer = (currentPlayer == Players.PLAYER_1) ? Players.PLAYER_2 : Players.PLAYER_1;

    void PlayAI()
    {
        countLeaf = 0;

        Node boardAI = new Node();
        Array.Copy(root.board, boardAI.board, root.board.Length);

        if (currentPlayer == Players.PLAYER_1)
        {
            GenerateTree(boardAI, 0, player1Color);
        }
        else
        {
            GenerateTree(boardAI, 0, player2Color);
        }

        if (countLeaf == 1)
        {
            Debug.Log("NUL");
        }

        bool isAIPlayed = false;
        int minMax = 0;
        while (!isAIPlayed)
        {

            if (currentPlayer == Players.PLAYER_1)
            {
                minMax = MinMaxAlgorithmAB(boardAI, int.MinValue, int.MaxValue, player1Color);
            }
            else
            {
                minMax = MinMaxAlgorithmAB(boardAI, int.MinValue, int.MaxValue, player2Color);
            }


            Node futureNode = boardAI.children.First(x => x.value == minMax);

            // Add visually the token to the grid
            for (uint y = 0; y < 6; y++)
            {
                for (uint x = 0; x < 7; x++)
                {
                    if (root.board[y, x] != futureNode.board[y, x])
                    {
                        Vector2 position = Vector2.zero;

                        position = new Vector2(x * 4 + 2, (5 - y) * 4);

                        if (currentPlayer == Players.PLAYER_1)
                        {
                            futureNode.ChangeStateToBoard(x, y, player1Color);

                            tokenCreator.CreateToken((int)x, (int)y, player1Color);
                        }
                        else if (currentPlayer == Players.PLAYER_2)
                        {
                            futureNode.ChangeStateToBoard(x, y, player2Color);

                            tokenCreator.CreateToken((int)x, (int)y, player2Color);
                        }

                        root = futureNode;
                        isAIPlayed = true;

                        break;
                    }
                }
            }
        }
        

        WinVerification();
        DrawVerification();

        if (!GameManager.Instance.IsGameIsFinished)
        {
            SwitchCurrentPlayer();
        }
    }

    void GenerateTree(Node _node, uint _currentDepth, Node.State _ownColor)
    {
        if (_node.IsAligned() != Node.State.Empty
            || _currentDepth > maxDepth)
        {
            countLeaf++;
            //if (countLeaf % 10000 == 0)
            //{
            //    Debug.Log(_node);
            //}
            return;
        }

        bool hasChild = false;

        // On crée l'arbre, colonne par colonne
        // On part du centre, et on se propage vers l'extérieur
        int currentColumn = 3;
        for (int x = 0; x < 4; x++)
        {
            for (int i = -1; i <= 1; i++)
            {
                if (i != 0)
                {
                    int realCurrentColumn = currentColumn + (x * i);

                    // If the column is full,  we can't add a new token so we don't check it
                    if (_node.GetFirstFreePositionInColumn((uint)realCurrentColumn) != -1)
                    {
                        // On commence la génération de l'arbre à partir du bas puisque la grille du P4 se fait selon la gravité
                        for (int y = 5; y >= 0; y--)
                        {
                            if (_node.board[y, realCurrentColumn] == Node.State.Empty)
                            {
                                Node newNode = new Node();

                                Array.Copy(_node.board, newNode.board, _node.board.Length);

                                newNode.isOpponent = !_node.isOpponent;

                                if (_ownColor == Node.State.Yellow_Token)
                                {
                                    newNode.board[y, realCurrentColumn] = newNode.isOpponent ? Node.State.Yellow_Token : Node.State.Red_Token;
                                }
                                else
                                {
                                    newNode.board[y, realCurrentColumn] = newNode.isOpponent ? Node.State.Red_Token : Node.State.Yellow_Token;
                                }

                                _node.children.Add(newNode);

                                hasChild = true;
                                GenerateTree(newNode, _currentDepth + 1, _ownColor);
                                y = -1;
                            }
                        }
                    }
                }
            }
        }

        if (!hasChild)
        {
            countLeaf++;

            //if (countLeaf % 10000 == 0)
            //{
            //    Debug.Log(_node);
            //}
        }
    }
    
    int MinMaxAlgorithm(Node _node, Node.State _ownColor)
    {
        if (_node.children.Count == 0)
        {
            _node.value = _node.Evaluate(_ownColor);
            return _node.value;
        }

        if (_node.isOpponent)
        {
            int min = int.MaxValue;
            for (int i = 0; i < _node.children.Count; i++)
            {
                int tmp = MinMaxAlgorithm(_node.children[i], _ownColor);
                min = Math.Min(min, tmp);
            }
            _node.value = min;
            return min;
        }
        else
        {
            int max = int.MinValue;
            for (int i = 0; i < _node.children.Count; i++)
            {
                int tmp = MinMaxAlgorithm(_node.children[i], _ownColor);
                max = Math.Max(max, tmp);
            }
            _node.value = max;
            return max;
        }
    }

    int MinMaxAlgorithmAB(Node _node, int _alphaScore, int _betaScore, Node.State _ownColor)
    {
        if (_node.children.Count == 0)
        {
            _node.value = _node.Evaluate(_ownColor);
            return _node.value;
        }

        if (_node.isOpponent)
        {   
            int min = int.MaxValue;
            for (int i = 0; i < _node.children.Count; i++)
            {
                min = Math.Min(min, MinMaxAlgorithmAB(_node.children[i], _alphaScore, _betaScore, _ownColor));

                if (min <= _alphaScore)
                {
                    _node.value = min;
                    return min;
                }

                _betaScore = Math.Min(_betaScore, min);
            }
            _node.value = min;
            return min;
        }
        else
        {
            int max = int.MinValue;
            for (int i = 0; i < _node.children.Count; i++)
            {
                max = Math.Max(max, MinMaxAlgorithmAB(_node.children[i], _alphaScore, _betaScore, _ownColor));

                if (max >= _betaScore)
                {
                    _node.value = max;
                    return max;
                }

                _alphaScore = Math.Max(_alphaScore, max);
            }
            _node.value = max;
            return max;
        }
    }

    void ResetBoard()
    {
        currentPlayer = Players.PLAYER_1;

        root.children.Clear();
        root.ResetBoard();

        UpdateGridToCurrentDisplay();
    }
}
