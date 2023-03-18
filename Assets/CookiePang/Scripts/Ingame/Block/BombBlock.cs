using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBlock : Block
{
   public override void Hit(int damage)
    {
        GameManager.instance.Explode(this);
        SoundManager.instance.PlaySound(1, "BombSound");
    }

    public override BlockData ToData(int row, int column)
    {
        BombBlockData data = new BombBlockData(hp, row, column);
        //data.position = transform.position;
        return data;
    }
}
