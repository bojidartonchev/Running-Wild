/**
 * Author: Sander Homan
 * Copyright 2012
 **/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Achievement;

class TestButton : MonoBehaviour
{
    private AchievementVariable<bool> boolClicked = new AchievementVariable<bool>("TestBoolAchiev");
    private AchievementVariable<float> floatClicked = new AchievementVariable<float>("TestFloatAchiev");
    private AchievementVariable<int> intClicked = new AchievementVariable<int>("TestIntAchiev");

    private Queue<AchievementDefinition> completedAchievementQueue = new Queue<AchievementDefinition>();
    private bool showingAchievement = false;
    private AchievementDefinition completedAchievement = null;
    private Vector2 completedAchievementBoxPosition = new Vector2(300,-200);

    void Start()
    {
        AchievementManager.Instance.onComplete += Instance_onComplete;
        AchievementManager.Instance.onFloatProgress += Instance_onFloatProgress;
        AchievementManager.Instance.onIntProgress += Instance_onIntProgress;
    }

    void Instance_onIntProgress(AchievementDefinition arg1, int arg2, int arg3)
    {
        Debug.Log("Made progress with the achievement " + arg1.title + ": " + arg3 + "/" + arg1.conditionIntValue);
    }

    void Instance_onFloatProgress(AchievementDefinition arg1, float arg2, float arg3)
    {
        Debug.Log("Made progress with the achievement " + arg1.title + ": " + arg3 + "/" + arg1.conditionFloatValue);
    }

    void Instance_onComplete(AchievementDefinition obj)
    {
        completedAchievementQueue.Enqueue(obj);
        if (!showingAchievement)
            StartCoroutine(ShowAchievementBox());
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
        while (t<1)
        {
            t += Time.deltaTime;
            completedAchievementBoxPosition.y = Mathf.Lerp(-200, 0, t);
            yield return null;
        }

        yield return new WaitForSeconds(2);

        // go out
        while (t>0)
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

        if (GUILayout.Button("Test bool Button"))
        {
            boolClicked.Value = true;
        }
        GUILayout.BeginHorizontal();
        GUILayout.Label("Value: " + floatClicked);
        if (GUILayout.Button("Test float Button"))
        {
            floatClicked.Value += 0.1f;
        }

        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
        GUILayout.Label("Value: " + intClicked);
        if (GUILayout.Button("Test int Button"))
        {
            intClicked.Value += 1;
        }
        GUILayout.EndHorizontal();

        if (GUILayout.Button("Reset Save(requires restart/refresh)"))
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
        }
    }
}

