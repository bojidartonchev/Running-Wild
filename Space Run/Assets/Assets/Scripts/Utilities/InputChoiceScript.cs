using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class InputChoiceScript : MonoBehaviour
{
    public GameObject choicePanel;
	// Use this for initialization
	void Start () {
        Invoke("Ask", 2);
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    private void Ask()
    {
        GameObject.Find("UISettingsObject").GetComponent<PauseScript>().PauseTime();
        this.choicePanel.SetActive(true);
    }

    public void ChooseTiltInput()
    {
        PlayerPrefs.SetInt("MovementInputType",0);
        GameObject.Find("Player").GetComponent<FirstPersonController>().LoadInputSelected();
    }

    public void ChooseTouchInput()
    {
        PlayerPrefs.SetInt("MovementInputType", 1);
        GameObject.Find("Player").GetComponent<FirstPersonController>().LoadInputSelected();
    }
}
