using System;
using UnityEngine;
using System.Collections;
using ProgressBar;
using UnityEngine.UI;

public class StatisticLoader : MonoBehaviour
{
    public Text highestScoreContainer;
    public Text distanceContainer;
    public Text partsContainer;


    // Use this for initialization
    void OnEnable ()
	{
	    this.LoadStatistic();
	}
	
	// Update is called once per frame
	void Update () {
        
    }

    private void LoadStatistic()
    {
        this.highestScoreContainer.text = PlayerPrefs.GetInt("highestScore").ToString();
        this.distanceContainer.text = PlayerPrefs.GetInt("DistanceCompleted") + " / 11";
        this.partsContainer.text = PlayerPrefs.GetInt("PartsCompleted") + " / 4";
    }
}
