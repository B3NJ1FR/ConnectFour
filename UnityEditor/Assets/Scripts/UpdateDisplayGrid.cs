using UnityEngine;

public class UpdateDisplayGrid : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnDisplayHasChanged += UpdateDisplay;
            GameManager.Instance.OnResetAsked += ClearBoardDisplayed;
        }

        UpdateDisplay();
    }
    private void OnDisable()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnDisplayHasChanged -= UpdateDisplay;
            GameManager.Instance.OnResetAsked -= ClearBoardDisplayed;
        }

    }

    void UpdateDisplay()
    {
        if (GameManager.Instance != null)
        {
            if (GameManager.Instance.CurrentDisplay == GameManager.Display.DISPLAY_3D)
            {
                if (gameObject.name == "Grid 3D")
                {
                    for (int i = 0; i < transform.childCount; i++)
                    {
                        transform.GetChild(i).gameObject.SetActive(true);
                    }
                }
                else if (gameObject.name == "Grid 2D")
                {
                    for (int i = 0; i < transform.childCount; i++)
                    {
                        if (transform.GetChild(i).gameObject.name != "Column Indication 2D")
                        {
                            transform.GetChild(i).gameObject.SetActive(false);
                        }
                    }
                }
            }
            else
            {
                if (gameObject.name == "Grid 3D")
                {
                    for (int i = 0; i < transform.childCount; i++)
                    {
                        if (transform.GetChild(i).gameObject.name != "Column Indication")
                        {
                            transform.GetChild(i).gameObject.SetActive(false);
                        }
                    }
                }
                else if (gameObject.name == "Grid 2D")
                {
                    for (int i = 0; i < transform.childCount; i++)
                    {
                        transform.GetChild(i).gameObject.SetActive(true);
                    }
                }

            }
        }
    }

    void ClearBoardDisplayed()
    {
        if (GameManager.Instance != null)
        {
            if (transform.GetChild(0).gameObject.name == "Tokens Container")
            {
                for (int i = 0; i < transform.GetChild(0).gameObject.transform.childCount; i++)
                {
                    Destroy(transform.GetChild(0).gameObject.transform.GetChild(i).gameObject);
                }
            }
            else if (transform.GetChild(0).gameObject.name == "Grid Tokens")
            {
                for (int i = 0; i < transform.GetChild(0).gameObject.transform.childCount; i++)
                {
                    Destroy(transform.GetChild(0).gameObject.transform.GetChild(i).gameObject);
                }
            }
        }
    }
}
