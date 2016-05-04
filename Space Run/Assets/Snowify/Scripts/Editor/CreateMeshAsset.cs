using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using System.IO;

public class CreateMeshAsset : EditorWindow
{
    [MenuItem("Tools/CreateMeshAsset")]
    static void Init()
    {
        // Get existing open window or if none, make a new one:
        CreateMeshAsset window = EditorWindow.GetWindow<CreateMeshAsset>("Create Mesh Asset");
        window.title = "Save Mesh(es)"; //to avoid unity warning
    }

    string folder = "Snowify/Meshes";
    bool rename = false;
    string newName = "";
    bool overrideExisting = false;

    void OnGUI()
    {
        GUIStyle splitter = new GUIStyle();
        splitter.border = new RectOffset(1, 1, 1, 1);
        splitter.stretchWidth = true;
        splitter.margin = new RectOffset(0, 0, 7, 7);

        GUILayout.Box("Save generated meshes as assets.\nThis is necessary if you want to use it in a prefab, or to reuse it in the scene.", GUILayout.ExpandWidth(true));
        GUILayout.Box("Select the GameObjects whose meshes you wish to save.", GUILayout.ExpandWidth(true));
        EditorGUILayout.Separator();
        folder = EditorGUILayout.TextField("Folder", folder);
        EditorGUILayout.Separator();
        rename = EditorGUILayout.Toggle("Rename Mesh", rename);
        if (rename)
            newName = EditorGUILayout.TextField("Mesh Name", newName);
        else
            EditorGUILayout.LabelField("Mesh will inherit GameObject's name");
        GUILayout.FlexibleSpace();
        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        if (GUILayout.Button("\nCreate Mesh Asset(s)\n"))
            CreateAsset();
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();
        GUILayout.FlexibleSpace();
    }

    void CreateAsset()
    {
        string[] folders = folder.Split(char.Parse("/"));
        string temppath = "";
        for (int i=0; i<folders.Length; i++)
        {
            if (!Directory.Exists(Application.dataPath + temppath + "/" + folders[i]))
            {
                if (i == 0)
                    AssetDatabase.CreateFolder("Assets", folders[i]);
                else
                    AssetDatabase.CreateFolder("Assets" + temppath, folders[i]);
            }
            temppath += "/" + folders[i];
        }

        GameObject[] sel = Selection.gameObjects;
        List<MeshFilter> meshfilters = new List<MeshFilter>();
        foreach (GameObject go in sel)
        {
            meshfilters.AddRange(go.GetComponentsInChildren<MeshFilter>());
        }
        foreach (MeshFilter mf in meshfilters)
        {
            if (!AssetDatabase.IsMainAsset(mf.sharedMesh) && !AssetDatabase.IsSubAsset(mf.sharedMesh))
            {
                string name = mf.name;// +"_mesh";
                if (rename)
                    name = newName;
                string path = "Assets/" + folder + "/" + name + ".asset";
                if (!overrideExisting)
                    path = AssetDatabase.GenerateUniqueAssetPath(path);
                AssetDatabase.CreateAsset(mf.sharedMesh, path);
            }
            else
                Debug.LogWarning("" + mf.sharedMesh + " is already an asset\nCreateMeshAsset Warning");
        }
    }
}
