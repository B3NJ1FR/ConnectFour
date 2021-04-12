using UnityEngine;
using UnityEngine.UI;

public class ChangeDisplayMode : MonoBehaviour
{
    public void ChangeDisplay()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.ChangeDisplayMode();

            if (GameManager.Instance.CurrentDisplay == GameManager.Display.DISPLAY_2D)
            {
                transform.GetChild(0).GetComponent<Text>().text = "3D";
            }
            else if (GameManager.Instance.CurrentDisplay == GameManager.Display.DISPLAY_3D)
            {
                transform.GetChild(0).GetComponent<Text>().text = "2D";
            }
        }

    }
}
