using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Unity.VisualScripting;
using static UnityEngine.GraphicsBuffer;
using System.Linq;

[CreateAssetMenu(menuName = "Stage / Create New Button Stage")]
public class ButtonStageAsset : StageAsset
{
    int initialButtonCount;
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
#if UNITY_EDITOR
        AssetDatabase.CreateAsset(stageAsset, "Assets/CookiePang/Stage/NewButtonStage.asset");
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
        initialButtonCount = GameManager.instance._buttons.Count;
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

    public override string GetGoal()
    {
        var results = from button in GameManager.instance._buttons
                      where button.pressed
                      select button;

        return "<size=150%><voffset=0.25em>" + "<sprite=3>" + "</voffset></size>" + results.Count() + " / " + initialButtonCount;
    }
}