using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ButtonScripts : MonoBehaviour
{
    public GameObject profileDropdownMenu;
    public GameObject gameDropdownMenu;

    // Use this for initialization
    void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void StartGameScene()
    {
        SceneManager.LoadScene(1);
    }

    public void ProfileDropdownToggle()
    {
       this.profileDropdownMenu.SetActive(!this.profileDropdownMenu.activeSelf);
    }

    public void GameDropdownToggle()
    {
        this.gameDropdownMenu.SetActive(!this.gameDropdownMenu.activeSelf);
    }
}
