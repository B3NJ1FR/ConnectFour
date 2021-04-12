using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FadeInOutManager : MonoBehaviour
{
    private Image fadeImage = null;
    private bool isFadeFinished = false;
    private int previousSceneID = -1;
    private float timer = 0.0f;
    private const float maxTimer = 2.5f;

    public void LaunchCustomScene(string _sceneName)
    {
        DontDestroyOnLoad(gameObject);

        fadeImage = gameObject.transform.GetChild(0).GetComponent<Image>();
        gameObject.GetComponent<Canvas>().worldCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        gameObject.GetComponent<Canvas>().sortingLayerName = "UI";
        gameObject.GetComponent<Canvas>().planeDistance = 1;

        previousSceneID = SceneManager.GetActiveScene().buildIndex;

        StartCoroutine(LoadAsyncScene(_sceneName));
        StartCoroutine(SceneFadeIn());
    }


    IEnumerator LoadAsyncScene(string _sceneName)
    {
        AsyncOperation asyncLoadNewScene = SceneManager.LoadSceneAsync(_sceneName);

        asyncLoadNewScene.allowSceneActivation = false;

        // Wait until the asynchronous scene fully loads
        while (asyncLoadNewScene.progress < 0.9f)
        {
            yield return null;
        }

        while (!isFadeFinished)
        {
            yield return null;
        }

        if (isFadeFinished)
        {
            // Scene changement
            asyncLoadNewScene.allowSceneActivation = true;

            while (SceneManager.GetActiveScene().buildIndex == previousSceneID)
            {
                yield return null;
            }

            gameObject.GetComponent<Canvas>().worldCamera = GameObject.Find("Main Camera").GetComponent<Camera>();

            timer = 0.0f;
            StartCoroutine(SceneFadeOut());
        }
    }

    IEnumerator SceneFadeIn()
    {
        while (timer < maxTimer)
        {
            timer += Time.deltaTime;

            fadeImage.color = new Color(1, 1, 1, timer / maxTimer);
            yield return null;
        }

        fadeImage.color = new Color(1, 1, 1, 1);

        isFadeFinished = true;
    }

    IEnumerator SceneFadeOut()
    {
        while (timer < maxTimer || fadeImage.color.a > 0.2f)
        {
            timer += Time.deltaTime;

            fadeImage.color = new Color(1, 1, 1, 1 - timer / maxTimer);
            yield return null;
        }

        fadeImage.color = new Color(1, 1, 1, 0);

        Destroy(gameObject);
    }
}
