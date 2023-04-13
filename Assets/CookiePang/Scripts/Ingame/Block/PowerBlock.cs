using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerBlock : Block
{
    public override void Hit(int damage)
    {
        GameManager.instance.DeleteBlock(this);
        GameManager.instance.ball.damage += 1;
    }

    public override void Shock(int damage)
    {
        GameManager.instance.DeleteBlock(this);
        GameManager.instance.ball.damage += 1;
    }

    protected override void Update()
    {
        
    }
    public override BlockData ToData(int row, int column)
    {
        PowerBlockData data = new PowerBlockData(hp, row, column);
        return data;
    }

}
