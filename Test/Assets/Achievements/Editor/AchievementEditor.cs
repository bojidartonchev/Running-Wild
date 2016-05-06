/**
 * Author: Sander Homan
 * Copyright 2012
 **/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using Achievement;

public class AchievementEditor : EditorWindow
{
    [System.Serializable]
    private class AchievementDefinitionInfo
    {
        public AchievementDefinition definition;
        public bool foldout = false;

        public AchievementDefinitionInfo(AchievementDefinition definition)
        {
            this.definition = definition;
        }
    }

    [System.Serializable]
    private class Category
    {
        public AchievementCategory category;
        public List<AchievementDefinitionInfo> achievements = new List<AchievementDefinitionInfo>();
        public bool foldout = false;
    }

    [MenuItem("Window/Achievements")]
    public static void openEditor()
    {
        GetWindow<AchievementEditor>("Achievements",true);
    }

    [SerializeField]
    private List<AchievementDefinitionInfo> definitions = null;
    [SerializeField]
    private Vector2 definitionScroll = Vector2.zero;
    [SerializeField]
    private AchievementDefinitions achievementDefinitions = null;
    [SerializeField]
    private AchievementDefinitionInfo selectedDefinition = null;
    [SerializeField]
    private float leftPanelWidth = 200;
    [SerializeField]
    private Rect splitterRect = new Rect();
    [SerializeField]
    private int splitterControlId = 0;
    [SerializeField]
    private Texture2D splitterIcon = null;
    [SerializeField]
    private Dictionary<int, Category> categories = new Dictionary<int, Category>();
    [SerializeField]
    private List<GUIContent> categoryPopup = new List<GUIContent>();
    [SerializeField]
    private Category selectedCategory = null;

    void OnGUI()
    {
        // list achievements
        if (definitions == null)
        {
            if (GUILayout.Button("Create Achievements"))
            {
                // check if file already exists, if so, display error
                if (AssetDatabase.LoadAssetAtPath("Assets/Resources/achievements.asset", typeof(AchievementDefinitions)) as AchievementDefinitions != null)
                {
                    EditorUtility.DisplayDialog("Error", "Achievements file already exists. Please reopen the achievement editor", "Close");
                    Close();
                    return;
                }

                // create new asset definitions file
                achievementDefinitions = ScriptableObject.CreateInstance<AchievementDefinitions>();
                if (!Directory.Exists(Application.dataPath + Path.DirectorySeparatorChar + "Resources"))
                    AssetDatabase.CreateFolder("Assets", "Resources");
                AssetDatabase.CreateAsset(achievementDefinitions, "Assets/Resources/Achievements.asset");
                AssetDatabase.SaveAssets();

                // reload instance
                OnEnable();
            }
        }
        else
        {
            // display definitions
            DisplayDefinitions();

            if (GUI.changed)
                EditorUtility.SetDirty(achievementDefinitions);
        }
    }

    void DisplayDefinitions()
    {
        EditorStyles.textField.wordWrap = true;
        // split window in 2
        GUILayout.BeginHorizontal();
        GUILayout.BeginVertical(GUILayout.MaxWidth(leftPanelWidth));
        GUILayout.Space(5);
        // show all items in a listbox
        definitionScroll = EditorGUILayout.BeginScrollView(definitionScroll, (GUIStyle)"box");

        foreach (Category category in categories.Values)
        {
            EditorGUILayout.BeginHorizontal();
            Rect foldoutRect = GUILayoutUtility.GetRect(new GUIContent(category.category.name), EditorStyles.foldout);
            Rect buttonRect = new Rect(foldoutRect.xMin + 15, foldoutRect.yMin, foldoutRect.width - 15, foldoutRect.height);
            foldoutRect.width = 15;
            category.foldout = EditorGUI.Foldout(foldoutRect, category.foldout, category.category.name);
            GUI.depth--;
            if (GUI.Button(buttonRect, "", (GUIStyle)"label"))
            {
                selectedCategory = category;
                selectedDefinition = null;
                GUI.FocusControl("");
            }
            GUI.depth++;

            EditorGUILayout.EndHorizontal();
            if (category.foldout)
            {
                foreach (AchievementDefinitionInfo definitionInfo in category.achievements)
                {
                    // show selectable name
                    if (definitionInfo != selectedDefinition)
                    {
                        if (GUILayout.Button(definitionInfo.definition.name, (GUIStyle)"label"))
                        {
                            selectedDefinition = definitionInfo;
                            EditorGUIUtility.keyboardControl = 0;
                        }
                    }
                    else
                        GUILayout.Label(definitionInfo.definition.name, (GUIStyle)"boldLabel");
                }
            }
        }


        EditorGUILayout.EndScrollView();
        // add - remove buttons
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("+"))
        {
            // add new definition
            AchievementDefinition newDef = new AchievementDefinition();
            newDef.name = "NewAchievement";
            achievementDefinitions.definitions.Add(newDef);
            AchievementDefinitionInfo info = new AchievementDefinitionInfo(newDef);
            definitions.Add(info);
            categories[0].achievements.Add(info);
            EditorUtility.SetDirty(achievementDefinitions);

            selectedDefinition = info;
        }
        if (GUILayout.Button("-"))
        {
            // remove selected definition
            achievementDefinitions.definitions.Remove(selectedDefinition.definition);
            EditorUtility.SetDirty(achievementDefinitions);

            definitions.Remove(selectedDefinition);
            selectedDefinition = null;
        }
        EditorGUILayout.EndHorizontal();
        if (GUILayout.Button("Add Category"))
        {
            // add category
            AchievementCategory cat = new AchievementCategory();
            cat.name = "NewCategory";
            cat.id = achievementDefinitions.maxCatId++;
            achievementDefinitions.categories.Add(cat);
            EditorUtility.SetDirty(achievementDefinitions);
            Category category = new Category();
            category.category = cat;
            categories.Add(category.category.id, category);
            BuildCategoryPopup();

            selectedCategory = category;
            selectedDefinition = null;
        }
        GUILayout.Space(5);
        GUILayout.EndVertical();
        // add splitter
        leftPanelWidth = Splitter(leftPanelWidth);


        GUILayout.BeginVertical();

        if (selectedDefinition != null)
        {
            // show definition info
            selectedDefinition.definition.name = EditorGUILayout.TextField("Name", selectedDefinition.definition.name);
            selectedDefinition.definition.title = EditorGUILayout.TextField("Title", selectedDefinition.definition.title);

            EditorGUI.BeginChangeCheck();
            Category oldCat = categories[selectedDefinition.definition.categoryId];
            selectedDefinition.definition.categoryId = EditorGUILayout.Popup(getPopupIndexFromCategory(categories[selectedDefinition.definition.categoryId]), categoryPopup.ToArray());
            if (EditorGUI.EndChangeCheck())
            {
                // show in correct category
                oldCat.achievements.Remove(selectedDefinition);
                categories[selectedDefinition.definition.categoryId].achievements.Add(selectedDefinition);
            }

            EditorGUILayout.LabelField("Description");
            selectedDefinition.definition.description = EditorGUILayout.TextArea(selectedDefinition.definition.description, GUILayout.Height(EditorStyles.textField.lineHeight * 3));

            EditorGUILayout.LabelField("Incomplete Description");
            selectedDefinition.definition.incompleteDescription = EditorGUILayout.TextArea(selectedDefinition.definition.incompleteDescription, GUILayout.Height(EditorStyles.textField.lineHeight * 3));

            selectedDefinition.definition.hidden = EditorGUILayout.Toggle("Hidden", selectedDefinition.definition.hidden);

            selectedDefinition.definition.type = (AchievementDefinition.Type)EditorGUILayout.EnumPopup("Type", selectedDefinition.definition.type);

            {
                // show conditions depending on type
                switch (selectedDefinition.definition.type)
                {
                    case AchievementDefinition.Type.Bool:
                        EditorGUILayout.BeginHorizontal();
                        EditorGUILayout.LabelField(new GUIContent("ConditionValue", "If checked, the variable needs to be true for the achievement to complete"));
                        // true false checkbox
                        selectedDefinition.definition.conditionBoolValue = EditorGUILayout.Toggle(selectedDefinition.definition.conditionBoolValue);
                        EditorGUILayout.EndHorizontal();
                        break;
                    case AchievementDefinition.Type.Float:
                        EditorGUILayout.BeginHorizontal();
                        EditorGUILayout.LabelField(new GUIContent("ConditionValue", "The variable needs to be higher then the value given for the achievement to complete"));
                        selectedDefinition.definition.conditionFloatValue = EditorGUILayout.FloatField(selectedDefinition.definition.conditionFloatValue);
                        EditorGUILayout.EndHorizontal();
                        selectedDefinition.definition.progressChangeSize = EditorGUILayout.FloatField(new GUIContent("Progress Change", "Will report progress when variable is divisable by the given value"), selectedDefinition.definition.progressChangeSize);
                        break;
                    case AchievementDefinition.Type.Int:
                        EditorGUILayout.BeginHorizontal();
                        EditorGUILayout.LabelField(new GUIContent("ConditionValue", "The variable needs to be higher then the value given for the achievement to complete"));
                        selectedDefinition.definition.conditionIntValue = EditorGUILayout.IntField(selectedDefinition.definition.conditionIntValue);
                        EditorGUILayout.EndHorizontal();
                        selectedDefinition.definition.progressChangeSize = EditorGUILayout.FloatField(new GUIContent("Progress Change", "Will report progress when variable is divisable by the given value"), selectedDefinition.definition.progressChangeSize);
                        break;
                }
            }
        }
        else if (selectedCategory != null)
        {
            EditorGUI.BeginChangeCheck();
            selectedCategory.category.name = EditorGUILayout.TextField("Category Name", selectedCategory.category.name);
            if (EditorGUI.EndChangeCheck())
            {
                BuildCategoryPopup();
            }
            if (selectedCategory.category.id == 0)
                GUI.enabled = false;
            if (GUILayout.Button("Remove"))
            {
                // remove category
                foreach (var achievement in selectedCategory.achievements)
                {
                    achievement.definition.categoryId = 0;
                    categories[0].achievements.Add(achievement);
                }
                categories.Remove(selectedCategory.category.id);

                selectedCategory = null;
                EditorUtility.SetDirty(achievementDefinitions);
            }
            if (selectedCategory==null || selectedCategory.category.id == 0)
                GUI.enabled = true;
        }

        GUILayout.EndVertical();
        GUILayout.EndHorizontal();

        if (GUI.changed)
            EditorUtility.SetDirty(achievementDefinitions);

        // HACK to unfocus controls
        GUI.SetNextControlName("");
    }

    private float Splitter(float width)
    {
        splitterRect = GUILayoutUtility.GetRect(new GUIContent(""), (GUIStyle)"box", GUILayout.Width(5), GUILayout.ExpandHeight(true));
        splitterControlId = GUIUtility.GetControlID(FocusType.Passive, splitterRect);
        EditorGUIUtility.AddCursorRect(splitterRect, MouseCursor.ResizeHorizontal);
        GUI.DrawTexture(new Rect(splitterRect.xMin, splitterRect.center.y - 5, splitterIcon.width, splitterIcon.height), splitterIcon);

        if (Event.current.type == EventType.mouseDown && splitterRect.Contains(Event.current.mousePosition))
        {
            GUIUtility.hotControl = splitterControlId;
            wantsMouseMove = true;
        }
        if (Event.current.type == EventType.mouseUp && GUIUtility.hotControl == splitterControlId)
        {
            GUIUtility.hotControl = 0;
            wantsMouseMove = false;
        }
        if (Event.current.type == EventType.mouseDrag && GUIUtility.hotControl == splitterControlId)
        {
            Repaint();
            return Mathf.Clamp(width + Event.current.delta.x, 100, this.position.width - 100);
        }
        return width;
    }

    void OnEnable()
    {
        // do init stuff;
        // build category list
        categories = new Dictionary<int, Category>();
        Category cat = new Category();
        cat.category = new AchievementCategory();
        cat.category.name = "Uncategorized";
        cat.category.id = 0;
        categories.Add(cat.category.id, cat);

        achievementDefinitions = AssetDatabase.LoadAssetAtPath("Assets/Resources/achievements.asset", typeof(AchievementDefinitions)) as AchievementDefinitions;
        if (achievementDefinitions != null)
        {
            // add categories from definition
            foreach (AchievementCategory category in achievementDefinitions.categories)
            {
                cat = new Category();
                cat.category = category;
                categories.Add(cat.category.id, cat);
            }

            definitions = new List<AchievementDefinitionInfo>();
            foreach (AchievementDefinition definition in achievementDefinitions.definitions)
            {
                AchievementDefinitionInfo info = new AchievementDefinitionInfo(definition);
                definitions.Add(info);
                
                // add to category
                categories[definition.categoryId].achievements.Add(info);
            }
        }

        splitterIcon = AssetDatabase.LoadAssetAtPath("Assets/Achievements/Editor/splitter.png", typeof(Texture2D)) as Texture2D;

        BuildCategoryPopup();
    }

    private void BuildCategoryPopup()
    {
        // build category popup
        categoryPopup = new List<GUIContent>();
        foreach (Category category in categories.Values)
        {
            categoryPopup.Add(new GUIContent(category.category.name, category.category.id.ToString()));
        }
    }

    Category getCategoryFromPopupIndex(int index)
    {
        int id = int.Parse(categoryPopup[index].tooltip);
        return categories[id];
    }

    int getPopupIndexFromCategory(Category cat)
    {
        for (int i=0; i<categoryPopup.Count; i++)
        {
            GUIContent gc = categoryPopup[i];
            if (gc.tooltip == cat.category.id.ToString())
            {
                return i;
            }
        }
        return 0;
    }
}

