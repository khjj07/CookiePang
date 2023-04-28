using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Unity.VisualScripting;
using static UnityEngine.GraphicsBuffer;

[CreateAssetMenu(menuName = "Stage / Create New Macaroon Stage")]
public class MacaroonStageAsset : StageAsset
{
    private int initailMacroonBlockCount;
    public MacaroonStageAsset()
    {
        scoreMode = ScoreMode.BallCount;
        gameMode = GameMode.Macaroon;
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
        AssetDatabase.CreateAsset(stageAsset, "Assets/CookiePang/Stage/NewMacaroonStage.asset");
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
        initailMacroonBlockCount = GameManager.instance._macaroon.Count;

    }

    public override bool IsClear()
    {
        if (GameManager.instance._macaroon.Count > 0)
        {
            return false;
        }
        return true;
    }

    public override string GetGoal()
    {
        return "<size=150%><voffset=0.2em>" + "<sprite=16>" + "</voffset></size>" +" HP : "+ GameManager.instance._macaroon[0].hp;
    }
}