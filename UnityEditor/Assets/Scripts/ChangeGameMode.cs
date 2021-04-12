using UnityEngine;

public class ChangeGameMode : MonoBehaviour
{
    public void ChooseGameMode(string _gameModeChoosen)
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.ChooseGameMode(_gameModeChoosen);
        }
    }
}
