using UnityEngine;
using System.Collections;
using ProgressBar;

public class ShipProgress : MonoBehaviour {

    public GameObject progressBar;
    public GameObject inventory;
    private int shipProgress;

    // Use this for initialization
    void Start ()
    {
        this.shipProgress = this.GetCurrentShipProgress();
        this.progressBar.GetComponent<ProgressRadialBehaviour>().Value = this.shipProgress;
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void UpdateProgress(string key)
    {
        int count = PlayerPrefs.GetInt(key);
        this.inventory.GetComponent<InventoryLoader>().ResetCount(key);
        this.shipProgress += count;
        this.SaveCurrentShipProgress(this.shipProgress);
        this.progressBar.GetComponent<ProgressRadialBehaviour>().IncrementValue(count);
    }

    private int GetCurrentShipProgress()
    {
        return PlayerPrefs.GetInt("shipProgress");
    }

    private void SaveCurrentShipProgress(int progress)
    {
        PlayerPrefs.SetInt("shipProgress",progress);
    }
}
