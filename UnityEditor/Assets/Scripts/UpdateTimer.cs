using UnityEngine;
using UnityEngine.UI;

public class UpdateTimer : MonoBehaviour
{
    float timer = 0.0f;
    Text timerText = null;
    bool isTimerCanContinue = true;

    // Start is called before the first frame update
    void Start()
    {
        timerText = GetComponent<Text>();

        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnResetAsked += RestartTimer;
            GameManager.Instance.OnWin += StopTimer;
        }
    }

    private void OnDisable()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnResetAsked -= RestartTimer;
            GameManager.Instance.OnWin -= StopTimer;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isTimerCanContinue) UpdateTimerText();
    }


    void StopTimer()
    {
        isTimerCanContinue = false;
    }

    void UpdateTimerText()
    {
        if (timerText != null)
        {
            timer += Time.deltaTime;

            // We convert the current time into a system composed of "Minutes : Seconds"
            int minutes = (int)timer / 60;
            int seconds = (int)timer % 60;


            if (seconds < 10)
            {
                if (minutes < 10)
                {
                    timerText.text = "0" + minutes + ":0" + seconds;
                }
                else
                {
                    timerText.text = "" + minutes + ":0" + seconds;
                }
            }
            else
            {
                if (minutes < 10)
                {
                    timerText.text = "0" + minutes + ":" + seconds;
                }
                else
                {
                    timerText.text = "" + minutes + ":" + seconds;
                }
            }
        }
    }

    void RestartTimer()
    {
        timer = 0.0f;
        isTimerCanContinue = true;

        UpdateTimerText();
    }
}
