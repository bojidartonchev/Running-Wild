using UnityEngine;
using System.Collections;
using Achievement;

public class AchievmentLoader : MonoBehaviour {

    AchievementVariable<int> metersAchievement;
    
    // Use this for initialization
    void Start () {
       metersAchievement = new AchievementVariable<int>("200meters");
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void AchievementComplete(AchievementDefinition def)
    {
        Debug.Log(metersAchievement.Value);
        Debug.Log("Achievement Complete: " + def.title);
    }
}
