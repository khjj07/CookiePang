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

[Serializable]
public class BombBlockData : BlockData
{
    public BombBlockData(int h, int r, int c) : base(h, r, c)
    {
        type = BlockType.BOMB;
    }
}

public class PowerBlockData : BlockData
{
    public PowerBlockData(int h, int r, int c) : base(h, r, c)
    {
        type = BlockType.POWER;
    }
}
public class JellyBlockData : BlockData
{
    public JellyBlockData(int h, int r, int c) : base(h, r, c)
    {
        type = BlockType.JELLY;
    }
}
public class PoisonBlockData : BlockData
{
    public PoisonBlockData(int h, int r, int c) : base(h, r, c)
    {
        type = BlockType.POISON;
    }
}

[Serializable]
public class HoleBlockData : BlockData
{
    public HoleBlockData(int r, int c) : base(0, r, c)
    {
        type = BlockType.HOLE;
    }
}

[Serializable]
public class CandyBlockData : BlockData
{
    public CandyBlockData(int r, int c) : base(0, r, c)
    {
        type = BlockType.CANDY;
    }
}

[Serializable]
public class ButtonBlockData : BlockData
{
    public ButtonBlockData(int r, int c) : base(0, r, c)
    {
        type = BlockType.BUTTON;
    }
}


[Serializable]
public class MacaroonBlockData : BlockData
{
    public MacaroonBlockData(int h, int r, int c) : base(h, r, c)
    {
        type = BlockType.MACAROON;
    }
}
