using UnityEngine;
using Achievement;
using System.Collections.Generic;
using Assets.Assets.Scripts.UI;
using Assets.Assets.Scripts.GameObjectScripts;
using System.Linq;

public class AchievementLoader : MonoBehaviour {

    public GameObject player;
    
    private List<AchievementVariable<int>> distance;
    private List<AchievementVariable<int>> parts;
    private AchievementDefinition completedAchievement = null;

    // Use this for initialization
    void Start () {
        
        distance = new List<AchievementVariable<int>>();
        distance.Add(new AchievementVariable<int>("100"));
        distance.Add(new AchievementVariable<int>("250"));
        distance.Add(new AchievementVariable<int>("500"));
        distance.Add(new AchievementVariable<int>("1250"));
        distance.Add(new AchievementVariable<int>("2500"));
        distance.Add(new AchievementVariable<int>("5000"));
        distance.Add(new AchievementVariable<int>("10000"));
        distance.Add(new AchievementVariable<int>("25000"));
        distance.Add(new AchievementVariable<int>("50000"));
        distance.Add(new AchievementVariable<int>("100000"));
        distance.Add(new AchievementVariable<int>("250000"));

        parts = new List<AchievementVariable<int>>();
        parts.Add(new AchievementVariable<int>("10p"));
        parts.Add(new AchievementVariable<int>("20p"));
        parts.Add(new AchievementVariable<int>("50p"));
        parts.Add(new AchievementVariable<int>("100p"));

        PlayerPrefs.SetInt("DistanceAchievementCount", distance.Count);
        PlayerPrefs.SetInt("PartsAchievementCount", parts.Count);

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
        foreach (AchievementVariable<int> prt in parts)
        {
            string temp = prt.identifier;
            temp = temp.Substring(0, temp.Length - 1);
            if (prt.Value <= int.Parse(temp))
            {
                prt.Value = (int)player.GetComponent<CharacterCollisionDetector>().CurrentRunParts;
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
            if(def.categoryId==1)
            {
                PlayerPrefs.SetInt("DistanceCompleted", PlayerPrefs.GetInt("DistanceCompleted") + 1);
            } else
            {
                PlayerPrefs.SetInt("PartsCompleted", PlayerPrefs.GetInt("PartsCompleted") + 1);
            }
            PlayerPrefs.SetInt(def.name, 1);
            PlayerPrefs.Save();
        }
    }
}
