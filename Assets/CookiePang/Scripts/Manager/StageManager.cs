using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class StageManager : Singleton<StageManager>
{
    private const string CURRNET_STAGE_KEY = "CURRNET_STAGE";
    private const string ACHIEVEMENT_RATE_ARRAY_KEY = "ACHIEVEMENT_RATE_ARRAY";

    public List<StageAsset> stageAssets;
    public StageAsset current;
    public GameObject selectButtonPrefab;

    public int currentIndex;
    public int lastStage;
    public List<int> achievementRateArray;

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
        achievementRateArray = new List<int>();
        foreach (var stage in Resources.LoadAll(_folderName))
        {
            stageAssets.Add((StageAsset)stage);
        }
        lastStage = PlayerPrefs.GetInt(CURRNET_STAGE_KEY, 1);
        currentIndex = lastStage;
        string achievementRateString = PlayerPrefs.GetString(ACHIEVEMENT_RATE_ARRAY_KEY, "0,0,0,0,0");
        var list = achievementRateString.Split(',');
        for (int i = 0; i < stageAssets.Count; i++)
        {
            if (list.Length > i)
            {
                achievementRateArray.Add(Int32.Parse(list[i]));
            }
            else
            {
                achievementRateArray.Add(0);
            }
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
                where stage.name.Equals("stage" + currentIndex)
                select stage;

        foreach (var stage in c)
        {
            current = stage;
            break;
        }
    }

    public GameObject CreateSelectButton(Vector3 pos, Transform trans, int count)
    {
        var obj = Instantiate(selectButtonPrefab, pos, Quaternion.Euler(0.0f, 0.0f, 0.0f), trans);
        obj.GetComponent<StageSelectButton>().myIndex = count + 1;
        if (count < lastStage)
        {
            obj.GetComponent<StageSelectButton>().isLocked = false;
            obj.GetComponent<StageSelectButton>().achievementRate = achievementRateArray[count];
        }
        else
        {
            obj.GetComponent<StageSelectButton>().isLocked = true;
            obj.GetComponent<StageSelectButton>().achievementRate = achievementRateArray[count];
        }

        return obj;
    }

    public void LastStageUp()
    {
        if (currentIndex == lastStage)
        {
            PlayerPrefs.SetInt(CURRNET_STAGE_KEY, ++lastStage);
        }

      
    }

    public void SetCurrentStageAchievementRate(int number)
    {
        if (achievementRateArray[currentIndex - 1] < number)
            achievementRateArray[currentIndex - 1] = number;

        string str="";
        foreach (var a in achievementRateArray)
        {
            str= str + a.ToString() + ",";
        }
        PlayerPrefs.SetString(ACHIEVEMENT_RATE_ARRAY_KEY, str);
    }

    
}
