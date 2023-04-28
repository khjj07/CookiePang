using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Unity.VisualScripting;
using static UnityEngine.GraphicsBuffer;

[CreateAssetMenu(menuName = "Stage / Create New Default Stage")]
public class DefaultStageAsset : StageAsset
{
    private int initailBlockCount;
    public DefaultStageAsset()
    {
        scoreMode = ScoreMode.BallCount;
        gameMode = GameMode.Default;
    }

    static public void CreateAsset(int initialBall, int[] stars, List<BlockData> blockData)
    {
        var stageAsset = CreateInstance<DefaultStageAsset>();
        stageAsset.blocks = blockData;
        stageAsset.initailBallCount = initialBall;
        stageAsset.stars[0] = stars[0];
        stageAsset.stars[1] = stars[1];
        stageAsset.stars[2] = stars[2];
#if UNITY_EDITOR
        AssetDatabase.CreateAsset(stageAsset, "Assets/CookiePang/Stage/NewDefaultStage.asset");
        AssetDatabase.Refresh();
#endif
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
        initailBlockCount = GameManager.instance._breakableBlocks.Count;
    }

    public override bool IsClear()
    {
        if (GameManager.instance._breakableBlocks.Count > 0)
        {
            return false;
        }
        return true;
    }

    public override string GetGoal()
    {
        return "<size=150%><voffset=0.25em>" + " <sprite=0>" + "</voffset></size> " + GameManager.instance._breakableBlocks.Count;
    }
}