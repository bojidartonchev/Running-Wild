using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DeviceSettings : MonoBehaviour
{
    public GameObject soundToggle;

	// Use this for initialization
	void Start () {
        // Enables screen dimming
        Screen.sleepTimeout = SleepTimeout.SystemSetting;

        if (PlayerPrefs.GetInt("firstPlay") == 0)
        {
            PlayerPrefs.SetFloat("volume", 1f);
            PlayerPrefs.SetInt("firstPlay",1);
        }

        AudioListener.volume = PlayerPrefs.GetFloat("volume");
        if (PlayerPrefs.GetFloat("volume") == 0)
	    {
	        this.soundToggle.GetComponent<Toggle>().isOn = false;
	    }
    }
	
	// Update is called once per frame
	void Update () {
       
    }

    public void ToggleSound()
    {        
        AudioListener.volume = 1 - AudioListener.volume;
        PlayerPrefs.SetFloat("volume", AudioListener.volume);
    }
}
