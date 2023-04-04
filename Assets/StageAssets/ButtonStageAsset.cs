using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Unity.VisualScripting;
using static UnityEngine.GraphicsBuffer;

[CreateAssetMenu(menuName = "Stage / Create New Button Stage")]
public class ButtonStageAsset : StageAsset
{
    public ButtonStageAsset()
    {
        scoreMode = ScoreMode.BallCount;
        gameMode = GameMode.HoleIn;
    }

    static public void CreateAsset(int initialBall, int[] stars, List<BlockData> blockData)
    {
        var stageAsset = CreateInstance<ButtonStageAsset>();
        stageAsset.blocks = blockData;
        stageAsset.initailBallCount = initialBall;
        stageAsset.stars[0] = stars[0];
        stageAsset.stars[1] = stars[1];
        stageAsset.stars[2] = stars[2];
        AssetDatabase.CreateAsset(stageAsset, "Assets/CookiePang/Stage/NewButtonStage.asset");
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
        foreach (var button in GameManager.instance._buttons)
        {
            if(!button.pressed)
            {
                return false; 
            }
        }
        return true;
    }
}