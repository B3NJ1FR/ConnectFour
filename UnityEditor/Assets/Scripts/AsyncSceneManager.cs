using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AsyncSceneManager : MonoBehaviour
{
    [SerializeField] GameObject prefabTransition = null;

    public void LaunchCustomScene(string _sceneName)
    {
        if (prefabTransition != null)
        {
            Instantiate(prefabTransition).GetComponent<FadeInOutManager>().LaunchCustomScene(_sceneName);
        }
    }
}
