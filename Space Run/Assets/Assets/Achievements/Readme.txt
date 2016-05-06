Introduction

Thank you for using the Achievements extension for the Unity 3D engine. The goal of this extension is to have an easy way to add achievements to your game or application.

Usage:
Defining the achievements in Unity

After installing the extension, the window menu has an extra item called Achievements. This opens the achievements window.

If you have not yet defined any achievements yet, there will be a button to create a new achievements file. This file is used to store the information on the achievements in the current project. After you have clicked the button, the window is divided into 2 parts. The left part shows all the defined achievements and the right part shows the information on the selected achievement. To add a new achievement, click the "+" button and to remove the selected achievement, click the "-" button. The right side of the window shows the fields of the achievements. Most fields are self-explanatory.

The most important field is the "Name" field. This field is used in the code to refer to the achievements. Other important fields are the "Type", "Condition Value" and "Progress Change" fields. Type can be Bool, Int or float. Condition Value is the value the code is checking against internally to see if the achievement as completed. For Bool the variable must be equal, but for Int and Float the variable must be equal or higher. The progress change is used to determine when to notify the game of progress made. If the variable is divisable by this value, then the progress event will fire.
Using the defined achievements

To use the achievements, you need to define a variable that uses the name identifier specified in unity. For a Bool achievement, use the bool type. For an Int or Float, use int or float respectively.

 AchievementVariable<bool> variable = new AchievementVariable<bool>("AchievementName");

To set the variable, set the Value property.

 variable.Value = true;

You can also use the variable like a normal variable when just reading.

 bool test = variable;

Subscribing to the events

To know when an achievement made progress or was completed, you need to subscribe to the achievement events.

  void Start()
  {
      AchievementManager.Instance.onComplete += AchievementComplete;
  }
  
  void AchievementComplete(AchievementDefinition def)
  {
      Debug.Log("Achievement Complete: " + def.title;
  }

For progress, there are 2 events. One for Int achievements and one for Float achievements. These events get 2 more arguments with the actualValue and the progressValue. The progressValue is always a multiple of the defined "Progress Change" field.

  void Start()
  {
      AchievementManager.Instance.onIntProgress += AchievementIntProgress;
  }
  
  void AchievementIntProgress(AchievementDefinition def, int actualValue, int progressValue)
  {
      Debug.Log("Achievement Progress: " + def.title + ":" + progressValue +"/" + def.conditionIntValue;
  }

To see more examples on how to use this extension, see the included html documentation.

Author:
    Sander Homan 

Copyright:
    Sander Homan 2012 

