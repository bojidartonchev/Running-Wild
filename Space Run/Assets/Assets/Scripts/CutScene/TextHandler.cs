using UnityEngine;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine.UI;

public class TextHandler : MonoBehaviour
{

    public GameObject textToActivate;


	// Use this for initialization
	void Start () {
	    Invoke("StartText",10f);
	}

    void StartText()
    {
        textToActivate.SetActive(true);
        GetComponent<UnityEngine.UI.Text>().text = "";
        Destroy(this);
    }
}
