using UnityEngine;

public class RestartGame : MonoBehaviour
{
    public void RestartTheGame()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.GameNeedToBeRestart();
        }
    }
}
