using UnityEngine;
using System.Collections;
using Achievement;
using System.Collections.Generic;
using Assets.Assets.Scripts.UI;

public class AchievementLoader : MonoBehaviour {

    public GameObject player;
    
    private List<AchievementVariable<int>> distance;
    private Queue<AchievementDefinition> completedAchievementQueue = new Queue<AchievementDefinition>();
    private bool showingAchievement = false;
    private AchievementDefinition completedAchievement = null;
    private Vector2 completedAchievementBoxPosition = new Vector2(600, 200);

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
        Debug.Log(def);
        if (PlayerPrefs.GetInt(def.name) != 1)
        {
            Debug.Log("Achievement Complete: " + def.title);
            Debug.Log("SAVING");
            completedAchievementQueue.Enqueue(def);
            /*if (!showingAchievement)
                StartCoroutine(ShowAchievementBox());
                */
            PlayerPrefs.SetInt(def.name, 1);
            PlayerPrefs.Save();
        }
    }

    IEnumerator ShowAchievementBox()
    {
        showingAchievement = true;

        while (showingAchievement)
        {
            completedAchievement = completedAchievementQueue.Dequeue();
            // show box
            yield return StartCoroutine(AnimateBox());
            if (completedAchievementQueue.Count == 0)
                showingAchievement = false;
        }
    }

    IEnumerator AnimateBox()
    {
        // come in
        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime;
            completedAchievementBoxPosition.y = Mathf.Lerp(-200, 0, t);
            yield return null;
        }

        yield return new WaitForSeconds(2);

        // go out
        while (t > 0)
        {
            t -= Time.deltaTime;
            completedAchievementBoxPosition.y = Mathf.Lerp(-200, 0, t);
            yield return null;
        }
    }

    void OnGUI()
    {
        if (showingAchievement)
        {
            // show achievement
            GUILayout.BeginArea(new Rect(completedAchievementBoxPosition.x, completedAchievementBoxPosition.y, 200, 100), (GUIStyle)"box");
            GUILayout.Label("Completed Achievement");
            GUILayout.Label(completedAchievement.title);
            GUILayout.Label(completedAchievement.description);
            GUILayout.EndArea();
        }
    }

}
