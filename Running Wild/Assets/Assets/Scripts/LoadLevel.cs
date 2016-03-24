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
        StartCoroutine(LoadLevelWithBar());
    }


    IEnumerator LoadLevelWithBar()
    {
        loadingImage.SetActive(true);
        async = SceneManager.LoadSceneAsync(1);
        while (!async.isDone)
        {
            loadingBar.value = async.progress;
            yield return null;
        }


    }
}