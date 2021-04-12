using UnityEngine;

public class ColumnSelection : MonoBehaviour
{
    public delegate void DelegateColumnSelectedHasChanged(int _columnNumber);
    public DelegateColumnSelectedHasChanged OnColumnSelectedHasChanged;

    public int previousColumnNumber = 0;

    static ColumnSelection instance = null;
    public static ColumnSelection Instance { get => instance; }

    void Awake()
    {
        // If we find an other GameManager in the current scene, we destroy it
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        // If it's the first time where we init this class, we give the address of the class to the Instance of the class
        instance = this;
        DontDestroyOnLoad(gameObject);
    }


    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            //Debug.DrawRay(ray.origin, ray.direction, Color.red, 5.0f);
            if (hit.transform.name.Contains("ConnectFour Case"))
            {
                int columnNumber = int.Parse(hit.transform.name.Substring(18, 1));

                if (columnNumber != previousColumnNumber)
                {
                    previousColumnNumber = columnNumber;

                    OnColumnSelectedHasChanged?.Invoke(columnNumber);
                }
            }
        }
    }
}
