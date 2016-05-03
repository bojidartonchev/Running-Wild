using UnityEngine;
using System.Collections;
using ProgressBar;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour
{

    public GameObject loadingBar;
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
            loadingBar.GetComponent<ProgressBarBehaviour>().IncrementValue(async.progress*100);
            yield return null;
        }


    }
}