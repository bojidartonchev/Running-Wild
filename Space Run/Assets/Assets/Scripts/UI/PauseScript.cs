using Assets.Assets.Scripts.GameObjectScripts;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class PauseScript : MonoBehaviour
{
    public Canvas pauseCanvas;
    
    private GameObject player;

    // Use this for initialization
    void Start()
    {
        this.player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Pause()
    {
        this.player.GetComponent<FirstPersonController>().enabled = false;
        this.pauseCanvas.enabled = true;
        this.PauseTime();
    }

    public void PauseTime()
    {
        Time.timeScale = 0;
    }

    public void Resume()
    {
        if (!this.player.GetComponent<CharacterCollisionDetector>().IsDead)
        {
            this.player.GetComponent<FirstPersonController>().enabled = true;
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
