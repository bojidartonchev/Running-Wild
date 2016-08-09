using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GarageSceneSettings : MonoBehaviour {

    //Scene transition
    public string nextScene;
    public Color myColor;

    public void EndGameCutScene()
    {
        Initiate.Fade(nextScene, myColor, 0.3f);
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

	// Use this for initialization
	void Start () {
        // Enables screen dimming
        Screen.sleepTimeout = SleepTimeout.SystemSetting;
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
