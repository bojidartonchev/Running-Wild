using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour
{

    public Slider loadingBar;
    public GameObject loadingImage;

    private AsyncOperation async;


    public void StartLoading()
    {
        int sceneToLoad = 3;
        if (PlayerPrefs.GetInt("introPlayed")==0)
        {
            sceneToLoad = 1;
        }
        loadingImage.SetActive(true);
        StartCoroutine(LoadLevelWithBar(sceneToLoad));
    }


    IEnumerator LoadLevelWithBar(int scene)
    {
        async = SceneManager.LoadSceneAsync(scene);
        while (!async.isDone)
        {
            loadingBar.value = async.progress;
            yield return null;
        }


    }
}