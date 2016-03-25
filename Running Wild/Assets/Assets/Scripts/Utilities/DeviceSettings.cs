using UnityEngine;

public class DeviceSettings : MonoBehaviour {

	// Use this for initialization
	void Start () {
        // Disable screen dimming
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();

            return;
        }
    }
}
