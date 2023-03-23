using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class StageManager : Singleton<StageManager>
{
    public List<StageAsset> stageAssets;
    public StageAsset current;
    // Start is called before the first frame update
    private void Awake()
    {
        stageAssets = new List<StageAsset>();
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        foreach (var stage in Resources.LoadAll("Stage"))
        {
            stageAssets.Add((StageAsset)stage);
        }
    }

    public StageAsset GetCurrentStage()
    {
        return current;
    }

    public void SetCurrentByName(string name)
    {
      var c = from stage in stageAssets
              where stage.name.Equals(name)
              select stage;

        foreach (var stage in c)
        {
           current= stage;
           break;
        }
    }
}
