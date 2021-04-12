using UnityEngine;
using UnityEngine.UI;

public class UpdateDisplayCurrentPlayer : MonoBehaviour
{
    [SerializeField] ConnectFourManager connectFourManager = null;
    [SerializeField] Sprite tokenYellow2D = null;
    [SerializeField] Sprite tokenRed2D = null;

    ConnectFourManager.Players previousPlayer = ConnectFourManager.Players.ANY_PLAYER;
    Image imagePlayerToken = null;
    Text textPlayerID = null;

    // Start is called before the first frame update
    void Start()
    {
        if (connectFourManager != null)
        {
            previousPlayer = connectFourManager.CurrentPlayer;
        }

        imagePlayerToken = GetComponent<Image>();
        textPlayerID = transform.GetChild(1).gameObject.GetComponent<Text>();

        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnResetAsked += ResetCurrentPlayer;
        }
    }



    private void OnDisable()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnResetAsked -= ResetCurrentPlayer;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (connectFourManager != null)
        {
            if (previousPlayer != connectFourManager.CurrentPlayer)
            {
                previousPlayer = connectFourManager.CurrentPlayer;

                if (previousPlayer == ConnectFourManager.Players.PLAYER_1)
                {
                    DisplayNewCurrentPlayer(1, connectFourManager.Player1Color);
                }
                else if (previousPlayer == ConnectFourManager.Players.PLAYER_2)
                {
                    DisplayNewCurrentPlayer(2, connectFourManager.Player2Color);
                }
            }
        }
    }

    void DisplayNewCurrentPlayer(uint _id, Node.State _color)
    {
        if (imagePlayerToken != null)
        {
            if (_color == Node.State.Red_Token)
            {
                imagePlayerToken.sprite = tokenRed2D;
            }
            else if (_color == Node.State.Yellow_Token)
            {
                imagePlayerToken.sprite = tokenYellow2D;
            }
        }

        if (_id > 0 && _id < 3)
        {
            textPlayerID.text = "" + _id;
        }
    }

    void ResetCurrentPlayer()
    {
        if (connectFourManager != null)
        {
            if (previousPlayer != connectFourManager.CurrentPlayer)
            {
                previousPlayer = connectFourManager.CurrentPlayer;

                if (previousPlayer == ConnectFourManager.Players.PLAYER_1)
                {
                    DisplayNewCurrentPlayer(1, connectFourManager.Player1Color);
                }
                else if (previousPlayer == ConnectFourManager.Players.PLAYER_2)
                {
                    DisplayNewCurrentPlayer(2, connectFourManager.Player2Color);
                }
            }
        }
    }
}
