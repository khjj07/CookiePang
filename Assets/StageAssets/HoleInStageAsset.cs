using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Unity.VisualScripting;
using System.Linq;

[CreateAssetMenu(menuName = "Stage / Create New Hole in Stage")]
public class HoleInStageAsset : StageAsset
{
    public int initialHoleBlockCount;
    public HoleInStageAsset()
    {
        scoreMode = ScoreMode.BallCount;
        gameMode = GameMode.HoleIn;
    }

    static public void CreateAsset(int initialBall, int[] stars, List<BlockData> blockData)
    {
        var stageAsset = CreateInstance<HoleInStageAsset>();
        stageAsset.blocks = blockData;
        stageAsset.initailBallCount = initialBall;
        stageAsset.stars[0] = stars[0];
        stageAsset.stars[1] = stars[1];
        stageAsset.stars[2] = stars[2];
#if UNITY_EDITOR
        AssetDatabase.CreateAsset(stageAsset, "Assets/CookiePang/Stage/NewHoleInStage.asset");
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
        initialHoleBlockCount=GameManager.instance._holes.Count;
    }

    public override bool IsClear()
    {
        foreach (var hole in GameManager.instance._holes)
        {
            if(!hole.holeIn)
            {
                return false; 
            }
        }
        return true;
    }

    public override string GetGoal()
    {
        var results = from hole in GameManager.instance._holes
                          where hole.holeIn
                          select hole;
        
        return "<size=150%><voffset=0.25em>" + "<sprite=14>" + "</voffset></size>" + " °ø³Ö±â";
    }
}