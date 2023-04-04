using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Unity.VisualScripting;
using static UnityEngine.GraphicsBuffer;
using System.Linq;

[CreateAssetMenu(menuName = "Stage / Create New Letter Stage")]
public class LetterStageAsset : StageAsset
{
    [SerializeField]
    private string _answer;
    public LetterStageAsset()
    {
        scoreMode = ScoreMode.BallCount;
        gameMode = GameMode.Candy;
    }

    static public void CreateAsset(int initialBall, int[] stars, List<BlockData> blockData)
    {
        var stageAsset = CreateInstance<LetterStageAsset>();
        stageAsset.blocks = blockData;
        stageAsset.initailBallCount = initialBall;
        stageAsset.stars[0] = stars[0];
        stageAsset.stars[1] = stars[1];
        stageAsset.stars[2] = stars[2];
        AssetDatabase.CreateAsset(stageAsset, "Assets/CookiePang/Stage/NewLetterStage.asset");
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
        string str = new string("");
        if(_answer.Equals(GameManager.instance._letters))
        {
            return true;
        }
        return false;
    }
}