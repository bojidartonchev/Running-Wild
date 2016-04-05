using UnityEngine;
using System.Collections;

public class FadeEffect : MonoBehaviour {

    CanvasGroup properties;
    // Use this for initialization
    void Start()
    {
        properties = this.GetComponent<CanvasGroup>();
        Invoke("StartFading",1f);
    }
    void StartFading()
    {
        StartCoroutine(Fade());
    }

    IEnumerator Fade()
    {
        while (properties.alpha < 1)
        {
            properties.alpha += Time.deltaTime / 6;
            yield return null;
        }
    }
}
