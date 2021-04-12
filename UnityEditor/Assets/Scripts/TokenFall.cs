using System.Collections;
using UnityEngine;

public class TokenFall : MonoBehaviour
{
    bool isFallCanContinue = true;
    Vector3 targetedPosition;
    RectTransform positionInUI2D = null;

    Node.State color = Node.State.Empty;
    public Node.State Color { get => color; }
    public Vector3 TargetedPosition { get => targetedPosition; }

    private void Start()
    {
        if (GameManager.Instance != null)
        {
            if (GameManager.Instance.CurrentDisplay == GameManager.Display.DISPLAY_3D)
            {
                StartCoroutine(TokenFalling3D());
            }
            else
            {
                positionInUI2D = GetComponent<RectTransform>();
                StartCoroutine(TokenFalling2D());
            }
        }

        if (FallingTokenList.Instance != null)
        {
            FallingTokenList.Instance.AddToken(this);
        }
    }

    public void SetTargetedPosition(Vector3 _position) => targetedPosition = _position;
    public void SetColor(Node.State _color) => color = _color;
  
    IEnumerator TokenFalling3D()
    {
        while (isFallCanContinue)
        {
            if (transform.position.y > targetedPosition.y)
            {
                Vector3 newPosition = transform.position;
                newPosition.y -= 30.0f * Time.deltaTime;
                transform.position = newPosition;

                if (transform.position.y <= targetedPosition.y)
                {
                    newPosition.y = targetedPosition.y;
                    transform.position = newPosition;

                    isFallCanContinue = false;

                    if (FallingTokenList.Instance != null)
                    {
                        FallingTokenList.Instance.RemoveToken(this);
                    }
                }
            }
            else
            {
                if (transform.position.y < targetedPosition.y)
                {
                    Vector3 newPosition = transform.position;
                    newPosition.y = targetedPosition.y;
                    transform.position = newPosition;

                    isFallCanContinue = false;

                    if (FallingTokenList.Instance != null)
                    {
                        FallingTokenList.Instance.RemoveToken(this);
                    }
                }
                else
                {
                    isFallCanContinue = false;

                    if (FallingTokenList.Instance != null)
                    {
                        FallingTokenList.Instance.RemoveToken(this);
                    }
                }
            }
            yield return null;

        }
        yield return null;
    }

    IEnumerator TokenFalling2D()
    {
        while (isFallCanContinue)
        {
            if (positionInUI2D != null)
            {
                if (positionInUI2D.localPosition.y > targetedPosition.y)
                {
                    Vector3 newPosition = positionInUI2D.localPosition;
                    newPosition.y -= 550.0f * Time.deltaTime;
                    positionInUI2D.localPosition = newPosition;

                    if (positionInUI2D.localPosition.y <= targetedPosition.y)
                    {
                        newPosition.y = targetedPosition.y;
                        positionInUI2D.localPosition = newPosition;

                        isFallCanContinue = false;

                        if (FallingTokenList.Instance != null)
                        {
                            FallingTokenList.Instance.RemoveToken(this);
                        }
                    }
                }
                else
                {
                    if (positionInUI2D.localPosition.y < targetedPosition.y)
                    {
                        Vector3 newPosition = positionInUI2D.localPosition;
                        newPosition.y = targetedPosition.y;
                        positionInUI2D.localPosition = newPosition;

                        isFallCanContinue = false;

                        if (FallingTokenList.Instance != null)
                        {
                            FallingTokenList.Instance.RemoveToken(this);
                        }
                    }
                    else
                    {
                        isFallCanContinue = false;

                        if (FallingTokenList.Instance != null)
                        {
                            FallingTokenList.Instance.RemoveToken(this);
                        }
                    }
                }
                yield return null;
            }
        }

        yield return null;
    }
}
