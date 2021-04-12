using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeInOut : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float waitBeforeFade = 10.0f;
    [SerializeField] private float timerOfFade = 2.5f;
    [Space(5)]
    [SerializeField] private bool isFadeInAsked = true;
    [SerializeField] private bool isFadeOutAsked = true;
    [SerializeField] private bool isFadeFromBeggining = true;
    [SerializeField] private bool isDestructionAsked = true;

    [Header("Data")]
    public bool isFadeFinished = false;

    private Image fadeImage = null;
    private float timer = 0.0f;

    private void Start()
    {
        fadeImage = GetComponent<Image>();

        if (isFadeFromBeggining)
        {
            if (isFadeInAsked)
            {
                LaunchFadeIn();
            }
            else
            {
                if (isFadeOutAsked)
                {
                    LaunchFadeOut();
                }
            }
        }
    }

    public void LaunchFadeIn()
    {
        StartCoroutine(FadeIn());
    }
    public void LaunchFadeOut()
    {
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeIn()
    {
        yield return new WaitForSeconds(waitBeforeFade);

        while (timer < timerOfFade)
        {
            timer += Time.deltaTime;

            fadeImage.color = new Color(1, 1, 1, timer / timerOfFade);
            yield return null;
        }

        fadeImage.color = new Color(1, 1, 1, 1);


        if (isFadeOutAsked)
        {
            timer = 0.0f;
            StartCoroutine(FadeOut());
        }
        else
        {
            if (isDestructionAsked)
            {
                StartCoroutine(DestroyObject());
            }
        }
    }

    IEnumerator FadeOut()
    {
        if (!isFadeInAsked && isFadeOutAsked)
        {   
            yield return new WaitForSeconds(waitBeforeFade);
        }

        while (timer < timerOfFade || fadeImage.color.a > 0.2f)
        {
            timer += Time.deltaTime;

            fadeImage.color = new Color(1, 1, 1, 1 - timer / timerOfFade);
            yield return null;
        }

        fadeImage.color = new Color(1, 1, 1, 0);

        isFadeFinished = true;

        if(isDestructionAsked)
        {
            StartCoroutine(DestroyObject());
        }
    }

    IEnumerator DestroyObject()
    {
        yield return new WaitForSeconds(0.5f);

        Destroy(gameObject);
    }
}
