using UnityEngine;
using System.Collections;

public class BloodSplat : MonoBehaviour {

    CanvasGroup properties;
    // Use this for initialization
    void Start () {
        properties = GameObject.Find("BloodScreen").GetComponent<CanvasGroup>();
        StartCoroutine(Fade());
    }

    IEnumerator Fade()
    {
        while (properties.alpha < 1) {
            properties.alpha += Time.deltaTime / 3;   
            yield return null;
        }
    }
}
