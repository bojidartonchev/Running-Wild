/**
 * Author: Sander Homan
 * Copyright 2012
 **/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Achievement;

public class AchievementSaveAndLoadTest : MonoBehaviour
{
    void Start()
    {
        // load the achievements; mark completed if already completed
        foreach (AchievementDefinition def in AchievementManager.Instance.Definitions)
        {
            if (PlayerPrefs.GetInt(def.name, 0) == 1)
                AchievementManager.Instance.MarkCompleted(def.name);
        }

        AchievementManager.Instance.onComplete += Instance_onComplete;
    }

    void Instance_onComplete(AchievementDefinition def)
    {
        // save the achievement as completed
        PlayerPrefs.SetInt(def.name, 1);
        PlayerPrefs.Save();
    }
}

