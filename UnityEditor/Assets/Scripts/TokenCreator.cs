using UnityEngine;

public class TokenCreator : MonoBehaviour
{
    [Header("Tokens 3D")]
    [SerializeField] GameObject redToken3D = null;
    [SerializeField] GameObject yellowToken3D = null;
    [SerializeField] GameObject grid3D = null;

    [Header("Tokens 2D")]
    [SerializeField] GameObject redToken2D = null;
    [SerializeField] GameObject yellowToken2D = null;
    [SerializeField] GameObject grid2D = null;

    public void Create3DToken(int _columnNumber, int _rowNumber, Node.State _tokenColor, bool _isDirectlyTeleported = false)
    {
        if (_tokenColor != Node.State.Empty)
        {
            Vector2 position = new Vector2(_columnNumber * 4 + 2, (5 - _rowNumber) * 4);
            GameObject GO = null;

            if (_isDirectlyTeleported)
            {
                if (_tokenColor == Node.State.Red_Token)
                {
                    GO = Instantiate(redToken3D, new Vector3(position.x, position.y, -0.5f), redToken3D.transform.rotation, grid3D.transform);
                }
                else if (_tokenColor == Node.State.Yellow_Token)
                {
                    GO = Instantiate(yellowToken3D, new Vector3(position.x, position.y, -0.5f), yellowToken3D.transform.rotation, grid3D.transform);
                }
            }
            else
            {
                if (_tokenColor == Node.State.Red_Token)
                {
                    GO = Instantiate(redToken3D, new Vector3(position.x, 24, -0.5f), redToken3D.transform.rotation, grid3D.transform);
                }
                else if (_tokenColor == Node.State.Yellow_Token)
                {
                    GO = Instantiate(yellowToken3D, new Vector3(position.x, 24, -0.5f), yellowToken3D.transform.rotation, grid3D.transform);
                }
            }
            

            if (GO != null)
            {
                GO.GetComponent<TokenFall>().SetTargetedPosition(new Vector3(position.x, position.y, -0.5f));
                GO.GetComponent<TokenFall>().SetColor(_tokenColor);
            }
        }
    }


    public void Create2DToken(int _columnNumber, int _rowNumber, Node.State _tokenColor, bool _isDirectlyTeleported = false)
    {
        if (_tokenColor != Node.State.Empty)
        {
            Vector2 position = new Vector2((_columnNumber * 125.0f) - 375.0f, ((5 - _rowNumber) * 125.0f) - 305.0f);
            GameObject GO = null;

            if (_tokenColor == Node.State.Red_Token)
            {
                GO = Instantiate(redToken2D, grid2D.transform);
                
            }
            else if (_tokenColor == Node.State.Yellow_Token)
            {
                GO = Instantiate(yellowToken2D, grid2D.transform);
            }

            if (GO != null)
            {
                if (_isDirectlyTeleported)
                {
                    GO.GetComponent<RectTransform>().localPosition = new Vector3(position.x, position.y, 0.0f);
                }
                else
                {
                    GO.GetComponent<RectTransform>().localPosition = new Vector3(position.x, 385.0f, 0.0f);
                }

                GO.GetComponent<TokenFall>().SetTargetedPosition(new Vector3(position.x, position.y, 0.0f));
                GO.GetComponent<TokenFall>().SetColor(_tokenColor);
            }
        }
    }

    public void CreateToken(int _columnNumber, int _rowNumber, Node.State _tokenColor, bool _isDirectlyTeleported = false)
    {
        if (GameManager.Instance != null)
        {
            if (GameManager.Instance.CurrentDisplay == GameManager.Display.DISPLAY_3D)
            {
                Create3DToken(_columnNumber, _rowNumber, _tokenColor, _isDirectlyTeleported);
            }
            else
            {
                Create2DToken(_columnNumber, _rowNumber, _tokenColor, _isDirectlyTeleported);
            }
        }
    }
}
