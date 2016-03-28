using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class PauseScript : MonoBehaviour
{
    public Canvas pauseCanvas;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Pause()
    {
        GameObject.Find("FPSController").GetComponent<FirstPersonController>().enabled = false;
        this.pauseCanvas.enabled = true;
        Time.timeScale = 0;
    }

    public void Resume()
    {
        if (!GameObject.Find("FPSController").GetComponent<CharacterCollisionDetector>().IsDead)
        {
            GameObject.Find("FPSController").GetComponent<FirstPersonController>().enabled = true;
        }
        this.pauseCanvas.enabled = false;
        Time.timeScale = 1;
    }

    void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            this.Pause();
        }
    }
}
