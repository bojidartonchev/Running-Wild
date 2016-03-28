using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ButtonFadeIn : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GetComponent<Image>().CrossFadeAlpha(1.0f, 2.5f,false);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
