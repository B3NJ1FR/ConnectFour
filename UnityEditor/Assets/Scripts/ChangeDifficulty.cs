using UnityEngine;

public class ChangeDifficulty : MonoBehaviour
{
    public void ChangeGameDifficulty(string _newDifficulty)
    {
        if (GameManager.Instance != null)
        {
            GameManager.GameDifficulty newDifficulty = GameManager.GameDifficulty.EASY;
            switch (_newDifficulty)
            {
                case "Easy":
                    newDifficulty = GameManager.GameDifficulty.EASY;
                    break;
                case "Medium":
                    newDifficulty = GameManager.GameDifficulty.MEDIUM;
                    break;
                case "Hard":
                    newDifficulty = GameManager.GameDifficulty.HARD;
                    break;
            }

            GameManager.Instance.CurrentDifficulty = newDifficulty;
        }
    }
}
