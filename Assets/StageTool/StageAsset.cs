using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public abstract class StageAsset : ScriptableObject
{
    [ReadOnlyAttribute]
    public StageMode mode;
    [ReadOnlyAttribute]
    public List<BlockData> blocks;

    public int initailBallCount;

    public virtual void Initialize()
    {
        foreach (var block in blocks)
        {
            var instance = GameManager.instance.CreateBlock(block.type);
            instance.transform.position = block.position;
            instance.hp = block.hp;
        }
        GameManager.instance.ballCount = initailBallCount;
    }
    public abstract bool IsClear();
    public abstract int GetStars();
}
