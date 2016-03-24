using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ButtonScripts : MonoBehaviour {

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void StartGameScene()
    {
        SceneManager.LoadScene(1);
    }
}
