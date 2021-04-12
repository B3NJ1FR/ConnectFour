using UnityEngine;
using UnityEngine.UI;

public class ColumnIndicationMovement : MonoBehaviour
{
    [SerializeField] RectTransform imageDisplay2D = null;
    public uint columnSelected = 0;

    private void Start()
    {
        if (ColumnSelection.Instance != null)
        {
            ColumnSelection.Instance.OnColumnSelectedHasChanged += ChangeColumnSelected;
        }

        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnDisplayHasChanged += UpdateDisplayHasChanged;

            UpdateDisplayHasChanged();
        }
    }

    private void OnDisable()
    {
        if (ColumnSelection.Instance != null)
        {
            ColumnSelection.Instance.OnColumnSelectedHasChanged -= ChangeColumnSelected;
        }

        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnDisplayHasChanged -= UpdateDisplayHasChanged;
        }

    }

    void ChangeColumnSelected(int _columnNumber)
    {
        columnSelected = (uint)_columnNumber;

        UpdateDisplayHasChanged();
    }

    void Display3DArrow()
    {
        if (transform.GetChild(0).gameObject.activeSelf == false
            && transform.GetChild(1).gameObject.activeSelf == false)
        {
            transform.GetChild(0).gameObject.SetActive(true);
            transform.GetChild(1).gameObject.SetActive(true);
        }
    }
    void Hide3DArrow()
    {
        if (transform.GetChild(0).gameObject.activeSelf == true
                    && transform.GetChild(1).gameObject.activeSelf == true)
        {
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(false);
        }
    }

    void Display2DArrow()
    {
        if (imageDisplay2D != null)
        {
            if (transform.GetChild(0).gameObject.GetComponent<Image>().enabled == false)
            {
                transform.GetChild(0).gameObject.GetComponent<Image>().enabled = true;
            }
        }
    }
    void Hide2DArrow()
    {
        if (imageDisplay2D != null)
        {
            if (transform.GetChild(0).gameObject.GetComponent<Image>().enabled == true)
            {
                transform.GetChild(0).gameObject.GetComponent<Image>().enabled = false;
            }
        }
    }

    void UpdateDisplayHasChanged()
    {
        if (GameManager.Instance != null)
        {
            if (GameManager.Instance.GameModeChoosen != GameManager.GameMode.AI_V_AI)
            {
                if (GameManager.Instance.CurrentDisplay == GameManager.Display.DISPLAY_3D)
                {
                    if (gameObject.name == "Column Indication")
                    {
                        Display3DArrow();

                        transform.position = new Vector3(columnSelected * 4 + 0.9f, transform.position.y, transform.position.z);
                    }
                    else if (gameObject.name == "Column Indication 2D")
                    {
                        Hide2DArrow();
                    }
                }
                else
                {
                    if (gameObject.name == "Column Indication")
                    {
                        Hide3DArrow();
                    }
                    else if (gameObject.name == "Column Indication 2D")
                    {
                        Display2DArrow();

                        if (imageDisplay2D != null)
                        {
                            imageDisplay2D.localPosition = new Vector3((columnSelected * 125.0f) - 375.0f, 0.0f, 0.0f);
                        }
                    }

                }
            }
            else
            {
                if (gameObject.name == "Column Indication")
                {
                    Hide3DArrow();
                }
                else if (gameObject.name == "Column Indication 2D")
                {
                    Hide2DArrow();
                }
            }
        }
    }
}

