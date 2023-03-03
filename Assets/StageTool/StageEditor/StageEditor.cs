using UnityEngine;
using UnityEditor;
using System;

#if UNITY_EDITOR
public class StageEditor : EditorWindow
{
    private Vector2 scrollPosition;
    private int[,] blocks = new int[9,9];
    private int blockKinds = 3;


   [MenuItem("Stage Tool/Stage Editor")]
    static void Init()
    {
        var window = (StageEditor)EditorWindow.GetWindow(typeof(StageEditor));
        window.Show();
    }

    private void OnGUI()
    {
        scrollPosition = GUILayout.BeginScrollView(scrollPosition, GUILayout.MinWidth(300), GUILayout.MaxWidth(500), GUILayout.MinHeight(300), GUILayout.MaxHeight(500));

        for (int i = 0; i < 9; i++)
        {
            GUILayout.BeginHorizontal();
            for (int j = 0; j < 9; j++)
            {
                if (GUILayout.Button("블록"+ (blocks[i,j]+1).ToString()))
                {
                    blocks[i, j] = (blocks[i, j] + 1) % blockKinds;
                }
            }
            GUILayout.EndHorizontal();
        }

        if (GUILayout.Button("Import"))
        {

        }

        if (GUILayout.Button("Export"))
        {

        }
        GUILayout.EndScrollView();
    }

    bool FileCheck(string fileName)
    {
        string[] a = AssetDatabase.FindAssets(fileName);

        if (a.Length > 0)
        {
            Debug.Log($"{fileName}과 같은 이름의 파일이 존재합니다. 파일명을 수정하세요");
            return true;
        }
        return false;
    }

    private void CreateScriptableObject<T>(string path) where T : ScriptableObject
    {
        var value = ScriptableObject.CreateInstance<T>();
        AssetDatabase.CreateAsset(value, "Assets/ScriptableObject" + path + "/New" + typeof(T).ToString() + ".asset");
        AssetDatabase.SaveAssets();

        EditorUtility.FocusProjectWindow();
        Selection.activeObject = value;
    }
}
#endif