using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerBlock : Block
{
    protected virtual void Update()
    {

    }

    public override void Hit(int damage)
    {
        GameManager.instance.DeleteBlock(this);
        GameManager.instance.ball.damage += 1;
    }

    public override BlockData ToData(int row, int column)
    {
        PowerBlockData data = new PowerBlockData(hp, row, column);
        return data;
    }

}
