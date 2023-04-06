using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonBlock : Block
{
    protected virtual void Update()
    {
       
    }
    public override void Hit(int damage)
    {
        GameManager.instance.DeleteBlock(this);
        GameManager.instance.GameOver();
        GameManager.instance.ball.isFloor = false;
    }

    public override void Shock(int damage)
    {
        GameManager.instance.DeleteBlock(this);
        GameManager.instance.GameOver();
        GameManager.instance.ball.isFloor = false;
    }

    public override BlockData ToData(int row, int column)
    {
        PoisonBlockData data = new PoisonBlockData(hp, row, column);
        //data.position = transform.position;
        return data;
    }
}
