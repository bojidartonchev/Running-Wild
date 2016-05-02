using UnityEngine;
using System.Collections;

public class CutScenePlanetHandler : MonoBehaviour {

    public string scene;
    public Color myColor;

    // Use this for initialization
    void Start () {
        Invoke("ActivateSpider", 13.5f);
        Invoke("ChangeLevel", 20f);
    }
	
	void ActivateSpider()
    {
        this.GetComponent<AnimalScript>().enabled = true;
        this.GetComponent<AudioSource>().Play();
    }

    void ChangeLevel()
    {
        PlayerPrefs.SetInt("introPlayed",1);
        Initiate.Fade(scene, myColor, 0.7f);
    }
}
