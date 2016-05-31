using UnityEngine;
using System.Collections;
using Achievement;
using System.Collections.Generic;
using Assets.Assets.Scripts.UI;

public class AchievementLoader : MonoBehaviour {

    public GameObject player;
    
    private List<AchievementVariable<int>> distance;
    private AchievementDefinition completedAchievement = null;

    // Use this for initialization
    void Start () {
        
        distance = new List<AchievementVariable<int>>();
        distance.Add(new AchievementVariable<int>("100"));
        distance.Add(new AchievementVariable<int>("250"));
        distance.Add(new AchievementVariable<int>("500"));
        distance.Add(new AchievementVariable<int>("1250"));
        distance.Add(new AchievementVariable<int>("2500"));
        
        AchievementManager.Instance.onComplete += AchievementComplete;

        foreach (AchievementDefinition def in AchievementManager.Instance.Definitions)
        {
            if (PlayerPrefs.GetInt(def.name, 0) == 1)
                AchievementManager.Instance.MarkCompleted(def.name);
        }
        //deletes achievement history
        PlayerPrefs.DeleteAll();
        AchievementManager.Instance.Clear();
    }
	
	// Update is called once per frame
	void Update () {
        foreach(AchievementVariable<int> dist in distance)
        {
            if (dist.Value <= int.Parse(dist.identifier)){
                dist.Value = (int)player.GetComponent<DistanceCounter>().Distance;
            }
        }
    }

    void AchievementComplete(AchievementDefinition def)
    {
        if (PlayerPrefs.GetInt(def.name) != 1)
        {
            //notifing
            Notifier.Instance.Notify(def.title,def.description,NotificationType.Achievement);
            
            //saving
            PlayerPrefs.SetInt(def.name, 1);
            PlayerPrefs.Save();
        }
    }
}
