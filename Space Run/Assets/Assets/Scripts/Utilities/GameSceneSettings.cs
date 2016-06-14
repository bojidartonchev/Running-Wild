using UnityEngine;
using System.Collections;
using Assets.Assets.Scripts.UI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSceneSettings : MonoBehaviour
{
    public GameObject soundToggle;

	// Use this for initialization
	void Start () {
        // Disables screen dimming
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        AudioListener.volume = PlayerPrefs.GetFloat("volume");
        if (PlayerPrefs.GetFloat("volume") == 0)
        {
            this.soundToggle.GetComponent<Toggle>().isOn = false;
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.Escape))
        {   GameObject.Find("Player").GetComponent<DistanceCounter>().SaveHighestScore();
            SceneManager.LoadScene(0);
            return;
        }
    }

    public void ToggleSound()
    {
        AudioListener.volume = 1 - AudioListener.volume;
        PlayerPrefs.SetFloat("volume", AudioListener.volume);
    }
}
