using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Unity.VisualScripting;
using static UnityEngine.GraphicsBuffer;

[CreateAssetMenu(menuName = "Stage / Create New Candy Stage")]
public class CandyStageAsset : StageAsset
{
    public CandyStageAsset()
    {
        scoreMode = ScoreMode.BallCount;
        gameMode = GameMode.Candy;
    }

    static public void CreateAsset(int initialBall, int[] stars, List<BlockData> blockData)
    {
        var stageAsset = CreateInstance<CandyStageAsset>();
        stageAsset.blocks = blockData;
        stageAsset.initailBallCount = initialBall;
        stageAsset.stars[0] = stars[0];
        stageAsset.stars[1] = stars[1];
        stageAsset.stars[2] = stars[2];
        AssetDatabase.CreateAsset(stageAsset, "Assets/CookiePang/Stage/NewCandyStage.asset");
        AssetDatabase.Refresh();
    }

    public override void Initialize()
    {
        base.Initialize();
        int count = 0;
        foreach (var star in stars)
        {
            GameManager.instance.stars[count] = star;
            count++;
        }

    }

    public override bool IsClear()
    {
        if (GameManager.instance._candies.Count <= 0)
        {
            return true;
        }
        return false;
    }
}