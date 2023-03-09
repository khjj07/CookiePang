using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Unity.VisualScripting;

[CreateAssetMenu(menuName = "Stage Tool/Create New Default Stage")]
public class DefaultStageAsset : StageAsset
{
    public int[] ballDeadLine = new int[3];

    public DefaultStageAsset()
    {
        scoreMode = ScoreMode.Default;
        gameMode = GameMode.Default;
    }

    static public void CreateAsset(int initialBall, int[] ballDeadLine, List<BlockData> blockData)
    {
        var stageAsset = CreateInstance<DefaultStageAsset>();
        stageAsset.blocks = blockData;
        stageAsset.initailBallCount = initialBall;
        stageAsset.ballDeadLine[0] = ballDeadLine[0];
        stageAsset.ballDeadLine[1] = ballDeadLine[1];
        stageAsset.ballDeadLine[2] = ballDeadLine[2];
        AssetDatabase.CreateAsset(stageAsset, "Assets/CookiePang/Stage/NewDefaultStage.asset");
        AssetDatabase.Refresh();
    }

    [MenuItem("Stage Tool/Load Stage Asset")]
    static public void LoadAsset()
    {
        var exampleAsset =
        AssetDatabase.LoadAssetAtPath<StageAsset>("Assets/CookiePang/Stage/NewStage.asset");
    }

    public override void Initialize()
    {
        base.Initialize();
    }

    public override bool IsClear()
    {
        foreach (var b in GameManager.instance.blocks)
        {
            if(b)
            {
                return false;
            }
        }
        return true;
    }

    public override int GetStars()
    {
        int stars = 0;
        foreach(var deadline in ballDeadLine)
        {
            if(deadline<GameManager.instance.ballCount)
            {
                stars++;
            }
        }
        return stars;
    }
}