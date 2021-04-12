using UnityEngine;
using UnityEngine.UI;

public class UpdateDifficultyText : MonoBehaviour
{
    GameManager.GameDifficulty currentDifficultyDisplayed;

    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.Instance != null)
        {
            currentDifficultyDisplayed = GameManager.Instance.CurrentDifficulty;
        }

        ChangeDifficultyDisplayed();
    }

    private void Update()
    {
        if (GameManager.Instance != null)
        {
            if (currentDifficultyDisplayed != GameManager.Instance.CurrentDifficulty)
            {
                currentDifficultyDisplayed = GameManager.Instance.CurrentDifficulty;

                ChangeDifficultyDisplayed();
            }
        }
        
    }

    public void ChangeDifficultyDisplayed()
    {
        switch (currentDifficultyDisplayed)
        {
            case GameManager.GameDifficulty.EASY:
                GetComponent<Text>().text = "EASY";
                break;
            case GameManager.GameDifficulty.MEDIUM:
                GetComponent<Text>().text = "MEDIUM";
                break;
            case GameManager.GameDifficulty.HARD:
                GetComponent<Text>().text = "HARD";
                break;
        }
    }
}
