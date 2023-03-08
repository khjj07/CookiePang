using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : Singleton<StageManager>
{
    public List<StageAsset> stageAssets;
    public int index = 0;
    // Start is called before the first frame update
    private void Awake()
    {
        stageAssets = new List<StageAsset>();
        DontDestroyOnLoad(this);
        foreach(var stage in Resources.LoadAll("Stage"))
        {
            stageAssets.Add((StageAsset)stage);
        }
    }

    public StageAsset GetCurrentStage()
    {
        if(stageAssets.Count>index)
            return stageAssets[index];
        else
            return null;
    }

    public void SetIndex(int val)
    {
        index = val;
    }
}
