using UnityEngine;
using System.Collections;

public class CutSceneDeviceSettings : MonoBehaviour {

	// Use this for initialization
	void Start () {
        // Disables screen dimming
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
