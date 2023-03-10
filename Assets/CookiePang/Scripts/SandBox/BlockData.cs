using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

[Serializable]
public class BlockData
{
    public BlockType type;
    public int hp;
    public int row;
    public int col;
    public BlockData(int h,int r, int c)
    {
        type = BlockType.DEFAULT;
        hp = h;
        row = r;
        col = c;
    }
}


[Serializable]
public class DefaultBlockData : BlockData
{
    public DefaultBlockData(int h, int r, int c) : base(h, r, c)
    {

    }
}

[Serializable]
public class TeleportationBlockData : BlockData
{
    public int destinationRow;
    public int destinationCol;
    public TeleportationBlockData(int h, int r, int c, int destr,int destc) : base(h, r, c)
    {
        type = BlockType.TELEPORT;
        destinationRow = destr;
        destinationCol = destc;
    }
}
