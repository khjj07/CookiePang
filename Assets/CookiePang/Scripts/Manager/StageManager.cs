using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.WSA;

public class StageManager : Singleton<StageManager>
{
    public List<StageAsset> stageAssets;
    public StageAsset current;
    public int currentIndex;
    public GameObject selectButtonPrefab;
    [SerializeField]
    private string _folderName = "Stage";
    // Start is called before the first frame update
    private void Awake()
    {
        stageAssets = new List<StageAsset>();
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        foreach (var stage in Resources.LoadAll(_folderName))
        {
            stageAssets.Add((StageAsset)stage);
        }
    }

    public StageAsset GetCurrentStage()
    {
        return current;
    }

    public void SetCurrent(int num)
    {
      currentIndex = num;
      var c = from stage in stageAssets
              where stage.name.Equals("stage"+ currentIndex)
              select stage;

        foreach (var stage in c)
        {
           current= stage;
           break;
        }
    }

    public GameObject CreateSelectButton(Vector3 pos,Transform trans,int count)
    {
        var obj = Instantiate(selectButtonPrefab, pos, Quaternion.Euler(0.0f, 0.0f, 0.0f) ,trans);
        obj.GetComponent<StageSelectButton>().myIndex = count+1;
        return obj;
    }
}
