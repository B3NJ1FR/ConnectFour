using UnityEngine;
using UnityEngine.UI;

public class WinDrawManager : MonoBehaviour
{
    void Start()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnWin += DisplayEndMessage;
        }
    }

    private void OnDisable()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnWin -= DisplayEndMessage;
        }
    }

    void DisplayEndMessage()
    {
        if (GameManager.Instance != null)
        {
            if (GameManager.Instance.IsGameIsFinished)
            {
                for (int i = 0; i < transform.childCount; i++)
                {
                    if (transform.GetChild(i).gameObject.name == "Win Message"
                        && (GameManager.Instance.CurrentGameStatus == GameManager.GameStatus.PLAYER_RED_WIN || GameManager.Instance.CurrentGameStatus == GameManager.GameStatus.PLAYER_YELLOW_WIN))
                    {
                        GameObject winMessageGO = transform.GetChild(i).gameObject;
                        winMessageGO.SetActive(true);

                        if (GameManager.Instance.CurrentGameStatus == GameManager.GameStatus.PLAYER_RED_WIN)
                        {
                            winMessageGO.transform.GetChild(0).GetComponent<Text>().text = "RED PLAYER WON";
                            winMessageGO.transform.GetChild(0).GetComponent<Text>().color = Color.red;
                        }
                        else
                        {
                            winMessageGO.transform.GetChild(0).GetComponent<Text>().text = "YELLOW PLAYER WON";
                            winMessageGO.transform.GetChild(0).GetComponent<Text>().color = Color.yellow;
                        }
                    }
                    else if (transform.GetChild(i).gameObject.name == "Draw Message"
                        && GameManager.Instance.CurrentGameStatus == GameManager.GameStatus.DRAW)
                    {
                        transform.GetChild(i).gameObject.SetActive(true);
                    }
                    
                    if (transform.GetChild(i).gameObject.name == "Button Back Main Menu")
                    {
                        transform.GetChild(i).gameObject.SetActive(true);
                    }
                }
            }
        }
    }
}
