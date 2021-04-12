using UnityEngine;

public class HideCanvas : MonoBehaviour
{
    public void HideCanvasOnClick()
    {
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            gameObject.transform.GetChild(i).gameObject.SetActive(false);
        }
    }
}
