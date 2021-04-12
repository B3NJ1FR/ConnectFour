using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public enum State
    {
        Empty,
        Red_Token,
        Yellow_Token
    }

    public State[,] board =
    {
        { State.Empty, State.Empty, State.Empty, State.Empty, State.Empty, State.Empty, State.Empty },
        { State.Empty, State.Empty, State.Empty, State.Empty, State.Empty, State.Empty, State.Empty },
        { State.Empty, State.Empty, State.Empty, State.Empty, State.Empty, State.Empty, State.Empty },
        { State.Empty, State.Empty, State.Empty, State.Empty, State.Empty, State.Empty, State.Empty },
        { State.Empty, State.Empty, State.Empty, State.Empty, State.Empty, State.Empty, State.Empty },
        { State.Empty, State.Empty, State.Empty, State.Empty, State.Empty, State.Empty, State.Empty },
    };

    public bool isOpponent = false;

    public List<Node> children = new List<Node>();

    public int value = 0;


    public State IsAligned()
    {
        // Lines' verification
        for (int y = 0; y < 6; y++)
        {
            for (int x = 0; x < 4; x++)
            {
                if (board[y, x] != State.Empty
                    && board[y, x + 1] == board[y, x]
                    && board[y, x + 2] == board[y, x]
                    && board[y, x + 3] == board[y, x])
                {
                    return board[y, x];
                }
            }
        }

        // Columns' verification
        for (int x = 0; x < 7; x++)
        {
            for (int y = 0; y < 3; y++)
            {
                if (board[y, x] != State.Empty
                    && board[y + 1, x] == board[y, x]
                    && board[y + 2, x] == board[y, x]
                    && board[y + 3, x] == board[y, x])
                {
                    return board[y, x];
                }
            }
        }



        // Diagonals to the right's verification
        for (int y = 0; y < 3; y++)
        {
            for (int x = 0; x < 4; x++)
            {
                if (board[y, x] != State.Empty
                    && board[y + 1, x + 1] == board[y, x]
                    && board[y + 2, x + 2] == board[y, x]
                    && board[y + 3, x + 3] == board[y, x])
                {
                    return board[y, x];
                }
            }
        }

        // Diagonals to the left's verification
        for (int y = 0; y < 3; y++)
        {
            for (int x = 3; x < 7; x++)
            {
                if (board[y, x] != State.Empty
                    && board[y + 1, x - 1] == board[y, x]
                    && board[y + 2, x - 2] == board[y, x]
                    && board[y + 3, x - 3] == board[y, x])
                {
                    return board[y, x];
                }
            }
        }

        return State.Empty;
    }

    //public int Evaluate()
    //{
    //    State result = IsAligned();
    //    if (result == State.Yellow_Token)
    //    {
    //        return 1;
    //    }
    //    else if (result == State.Red_Token)
    //    {
    //        return -1;
    //    }
    //    else
    //    {
    //        return 0;
    //    }
    //}


    public int Evaluate(State _ownColor)
    {
        State result = IsAligned();

        if (result == State.Yellow_Token)
        {
            return (_ownColor == State.Yellow_Token) ? 100 : -100;
        }
        else if (result == State.Red_Token)
        {
            return (_ownColor == State.Yellow_Token) ? -100 : 100;
        }
        else
        {
            if (GameManager.Instance != null)
            {
                if (GameManager.Instance.CurrentDifficulty == GameManager.GameDifficulty.MEDIUM)
                {
                    return MediumEvaluation(_ownColor);
                }
                else if (GameManager.Instance.CurrentDifficulty == GameManager.GameDifficulty.HARD)
                {
                    return HighEvaluation(_ownColor);
                }
            }

            return 0;
        }
    }

    public int MediumEvaluation(State _ownColor)
    {
        int score = 0;

        // Lines' verification
        for (int y = 0; y < 6; y++)
        {
            for (int x = 0; x < 4; x++)
            {
                if (board[y, x] == State.Empty
                    && board[y, x + 1] != State.Empty
                    && board[y, x + 2] == board[y, x + 1]
                    && board[y, x + 3] == board[y, x + 1])
                {
                    score += (board[y, x + 1] == _ownColor) ? 45 : -45;
                }
                else if (board[y, x] != State.Empty
                    && board[y, x + 1] == State.Empty
                    && board[y, x + 2] == board[y, x]
                    && board[y, x + 3] == board[y, x])
                {
                    score += (board[y, x] == _ownColor) ? 45 : -45;
                }
                else if (board[y, x] != State.Empty
                    && board[y, x + 1] == board[y, x]
                    && board[y, x + 2] == State.Empty
                    && board[y, x + 3] == board[y, x])
                {
                    score += (board[y, x] == _ownColor) ? 45 : -45;
                }
                else if (board[y, x] != State.Empty
                    && board[y, x + 1] == board[y, x]
                    && board[y, x + 2] == board[y, x]
                    && board[y, x + 3] == State.Empty)
                {
                    score += (board[y, x] == _ownColor) ? 45 : -45;
                }
            }
        }

        // Columns' verification
        for (int x = 0; x < 7; x++)
        {
            for (int y = 0; y < 3; y++)
            {
                if (board[y, x] == State.Empty
                    && board[y + 1, x] != State.Empty
                    && board[y + 2, x] == board[y + 1, x]
                    && board[y + 3, x] == board[y + 1, x])
                {
                    score += (board[y + 1, x] == _ownColor) ? 70 : -70;
                }
            }
        }

        return score;
    }
    
    public int HighEvaluation(State _ownColor)
    {
        int score = 0;

        // Lines' verification
        for (int y = 0; y < 6; y++)
        {
            for (int x = 0; x < 4; x++)
            {
                if (board[y, x] == State.Empty
                    && board[y, x + 1] != State.Empty
                    && board[y, x + 2] == board[y, x + 1]
                    && board[y, x + 3] == board[y, x + 1])
                {
                    score += (board[y, x + 1] == _ownColor) ? 45 : -45;
                }
                else if (board[y, x] != State.Empty
                    && board[y, x + 1] == State.Empty
                    && board[y, x + 2] == board[y, x]
                    && board[y, x + 3] == board[y, x])
                {
                    score += (board[y, x] == _ownColor) ? 45 : -45;
                }
                else if (board[y, x] != State.Empty
                    && board[y, x + 1] == board[y, x]
                    && board[y, x + 2] == State.Empty
                    && board[y, x + 3] == board[y, x])
                {
                    score += (board[y, x] == _ownColor) ? 45 : -45;
                }
                else if (board[y, x] != State.Empty
                    && board[y, x + 1] == board[y, x]
                    && board[y, x + 2] == board[y, x]
                    && board[y, x + 3] == State.Empty)
                {
                    score += (board[y, x] == _ownColor) ? 45 : -45;
                }
            }
        }

        // Columns' verification
        for (int x = 0; x < 7; x++)
        {
            for (int y = 0; y < 3; y++)
            {
                if (board[y, x] == State.Empty
                    && board[y + 1, x] != State.Empty
                    && board[y + 2, x] == board[y + 1, x]
                    && board[y + 3, x] == board[y + 1, x])
                {
                    score += (board[y + 1, x] == _ownColor) ? 70 : -70;
                }
            }
        }



        // Diagonals to the right's verification
        for (int y = 0; y < 3; y++)
        {
            for (int x = 0; x < 4; x++)
            {
                if (board[y, x] == State.Empty
                    && board[y + 1, x + 1] != State.Empty
                    && board[y + 2, x + 2] == board[y + 1, x + 1]
                    && board[y + 3, x + 3] == board[y + 1, x + 1])
                {
                    score += (board[y + 1, x + 1] == _ownColor) ? 35 : -35;
                }
                else if (board[y, x] != State.Empty
                   && board[y + 1, x + 1] == State.Empty
                   && board[y + 2, x + 2] == board[y, x]
                   && board[y + 3, x + 3] == board[y, x])
                {
                    score += (board[y, x] == _ownColor) ? 35 : -35;
                }
                else if (board[y, x] != State.Empty
                   && board[y + 1, x + 1] == board[y, x]
                   && board[y + 2, x + 2] == State.Empty
                   && board[y + 3, x + 3] == board[y, x])
                {
                    score += (board[y, x] == _ownColor) ? 35 : -35;
                }
                else if (board[y, x] != State.Empty
                   && board[y + 1, x + 1] == board[y, x]
                   && board[y + 2, x + 2] == board[y, x]
                   && board[y + 3, x + 3] == State.Empty)
                {
                    score += (board[y, x] == _ownColor) ? 35 : -35;
                }
            }
        }

        // Diagonals to the left's verification
        for (int y = 0; y < 3; y++)
        {
            for (int x = 3; x < 7; x++)
            {
                if (board[y, x] == State.Empty
                    && board[y + 1, x - 1] != State.Empty
                    && board[y + 2, x - 2] == board[y + 1, x - 1]
                    && board[y + 3, x - 3] == board[y + 1, x - 1])
                {
                    score += (board[y + 1, x - 1] == _ownColor) ? 35 : -35;
                }
                else if (board[y, x] != State.Empty
                    && board[y + 1, x - 1] == State.Empty
                    && board[y + 2, x - 2] == board[y, x]
                    && board[y + 3, x - 3] == board[y, x])
                {
                    score += (board[y, x] == _ownColor) ? 35 : -35;
                }
                else if (board[y, x] != State.Empty
                    && board[y + 1, x - 1] == board[y, x]
                    && board[y + 2, x - 2] == State.Empty
                    && board[y + 3, x - 3] == board[y, x])
                {
                    score += (board[y, x] == _ownColor) ? 35 : -35;
                }
                else if (board[y, x] != State.Empty
                    && board[y + 1, x - 1] == board[y, x]
                    && board[y + 2, x - 2] == board[y, x]
                    && board[y + 3, x - 3] == State.Empty)
                {
                    score += (board[y, x] == _ownColor) ? 35 : -35;
                }
            }
        }

        return score;
    }

    public override string ToString()
    {
        string str = "";
        for (int i = 0; i < 6; i++)
        {
            for (int j = 0; j < 7; j++)
            {
                switch (board[i,j])
                {
                    case State.Empty:
                        str += "-";
                        break;
                    case State.Red_Token:
                        str += "R";
                        break;
                    case State.Yellow_Token:
                        str += "Y";
                        break;
                    default:
                        break;
                }
            }
            str += "\n";
        }
        return str;
    }

    public bool IsColumnFreeForNewToken(uint _columnNumber)
    {
         return (board[0, _columnNumber] == State.Empty) ? true : false;
    }
    public int GetFirstFreePositionInColumn(uint _columnNumber)
    {
        for (int y = 5; y >= 0; y--)
        {
            if (board[y, _columnNumber] == State.Empty)
            {
                return y;
            }
        }

        return -1;
    }
    public int ChangeStateToBoard(uint _columnNumber, uint _rowNumber, State _newState)
    {
        if (board[_rowNumber, _columnNumber] == State.Empty)
        {
            board[_rowNumber, _columnNumber] = _newState;
            return 1;
        }
        else
        {
            return -1;
        }
    }

    public void ResetBoard()
    {
        for (int i = 0; i < 6; i++)
        {
            for (int j = 0; j < 7; j++)
            {
                board[i, j] = State.Empty;
            }
        }
    }
}
