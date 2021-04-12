using System.Collections.Generic;
using UnityEngine;

public class FallingTokenList : MonoBehaviour
{
    static FallingTokenList instance = null;
    public static FallingTokenList Instance { get => instance; }

    List<TokenFall> listOfCurrentFallingTokens;
    public List<TokenFall> ListOfCurrentFallingTokens { get => listOfCurrentFallingTokens; }

    // Start is called before the first frame update
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

        listOfCurrentFallingTokens = new List<TokenFall>();
    }


    private void Start()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnResetAsked += ClearContent;
        }
    }

    private void OnDisable()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnResetAsked -= ClearContent;
        }
    }

    public void AddToken(TokenFall _token)
    {
        listOfCurrentFallingTokens.Add(_token);
    }

    public void RemoveToken(TokenFall _token)
    {
        listOfCurrentFallingTokens.Remove(_token);
    }

    public void ClearContent()
    {
        foreach (var token in listOfCurrentFallingTokens)
        {
            if (token != null)
            {
                GameObject currentToken = token.gameObject;

                if (currentToken != null)
                {
                    Destroy(currentToken);
                }
            }
        }
        
        listOfCurrentFallingTokens.Clear();
    }
}
