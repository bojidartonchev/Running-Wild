using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GarageSceneSettings : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene(3);

            return;
        }
    }
}
