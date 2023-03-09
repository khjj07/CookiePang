using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public enum BlockType
{
    NONE,
    DEFAULT,
    TELEPORT,
    BOMB
}

[Serializable]
public class BlockData
{
    public Vector3 position;
    public BlockType type;
    public int hp;
    public int row;
    public int col;
}
