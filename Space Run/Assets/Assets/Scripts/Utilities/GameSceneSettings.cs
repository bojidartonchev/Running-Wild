using UnityEngine;
using System.Collections;
using Assets.Assets.Scripts.UI;
using UnityEngine.SceneManagement;

public class GameSceneSettings : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.Escape))
        {   GameObject.Find("Player").GetComponent<DistanceCounter>().SaveHighestScore();
            SceneManager.LoadScene(0);
            return;
        }
    }
}
