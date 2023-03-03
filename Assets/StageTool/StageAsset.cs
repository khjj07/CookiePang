using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class StageAsset : ScriptableObject
{
    public int[] starDeadLine = new int[3];

   [MenuItem("Stage Tool/Create New Stage Asset")]
    static void CreateStageAsset()
    {
        var stageAsset = CreateInstance<StageAsset>();

        AssetDatabase.CreateAsset(stageAsset, "Assets/Editor/Stage.asset");
        AssetDatabase.Refresh();
    }

    [MenuItem("Stage Tool/Load Stage Asset")]
    static void LoadStageAsset()
    {
        var exampleAsset =
        AssetDatabase.LoadAssetAtPath<StageAsset>("Assets/Editor/ExampleAsset.asset");
    }
}
