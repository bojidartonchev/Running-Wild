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
        loadingImage.SetActive(true);
        StartCoroutine(LoadLevelWithBar());
    }


    IEnumerator LoadLevelWithBar()
    {
        async = SceneManager.LoadSceneAsync(1);
        while (!async.isDone)
        {
            loadingBar.value = async.progress;
            yield return null;
        }


    }
}