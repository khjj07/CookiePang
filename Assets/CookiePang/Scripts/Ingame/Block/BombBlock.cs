using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBlock : Block
{
   public override void Hit(int damage)
    {
        GameManager.instance.Explode(this);
    }

    public override BlockData ToData(int row, int column)
    {
        BombBlockData data = new BombBlockData(hp, row, column);
        //data.position = transform.position;
        return data;
    }
}
