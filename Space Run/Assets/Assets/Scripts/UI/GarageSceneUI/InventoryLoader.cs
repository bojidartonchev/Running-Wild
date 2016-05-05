using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InventoryLoader : MonoBehaviour
{

    public GameObject leftWingContainer;
    public GameObject rightWingContainer;
    public GameObject engineContainer;

	// Use this for initialization
	void Start ()
	{
        this.UpdateContainers();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ResetCount(string key)
    {
        PlayerPrefs.SetInt(key,0);
        this.UpdateContainers();
    }

    private void UpdateContainers()
    {
        this.FillCont(leftWingContainer, "LeftWingParts");
        this.FillCont(rightWingContainer, "RightWingParts");
        this.FillCont(engineContainer, "EngineParts");
    }

    private void FillCont(GameObject cont, string key)
    {
        int currentCount = PlayerPrefs.GetInt(key);
        cont.transform.Find("Count").GetComponent<Text>().text = currentCount.ToString();

        if (currentCount == 0)
        {
            Transform image = cont.transform.Find("Drag Image");
            image.GetComponent<DragMe>().enabled = false;
            image.GetComponent<Image>().color = new Color32(225,225,225,100);
        }
    }
}
