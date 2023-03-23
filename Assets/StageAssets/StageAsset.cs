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

    private void OnEnable()
    {
        EditorUtility.SetDirty(this);
    }

    public virtual void Initialize()
    {
        GameManager.instance.initialBallCount = initailBallCount;
        GameManager.instance.ballCount = initailBallCount;
        foreach (var block in blocks)
        {
            var instance = GameManager.instance.CreateBlock(block.type, block.row, block.col);
            instance.GetData(block);
        }
    }
    public abstract bool IsClear();
    public abstract int GetStars();
}
