using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEngine.UI.CanvasScaler;

public enum ScoreMode
{
    Default,
    Score
}

public enum GameMode
{
    Default
}

public abstract class StageAsset : ScriptableObject
{
    public ScoreMode scoreMode;
    [ReadOnlyAttribute]
    public GameMode gameMode;
    [ReadOnlyAttribute]
    public List<BlockData> blocks;

    public int initailBallCount;


    public StageAsset()
    {
        scoreMode = ScoreMode.Default;
        gameMode = GameMode.Default;
    }

    public virtual void Initialize()
    {
        foreach (var block in blocks)
        {
            var instance = GameManager.instance.CreateBlock(block.type, block.row, block.col);
            instance.hp = block.hp;
        }
        GameManager.instance.ballCount = initailBallCount;
    }
    public abstract bool IsClear();
    public abstract int GetStars();
}
