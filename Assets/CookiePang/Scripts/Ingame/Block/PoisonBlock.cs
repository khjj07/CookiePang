using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonBlock : Block
{
    public override void Hit(int damage)
    {
        GameManager.instance.DeleteBlock(this);
        GameManager.instance.GameOver();
        GameManager.instance.ball.isFloor = false;
        GameManager.instance.ball.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }

    public override BlockData ToData(int row, int column)
    {
        PoisonBlockData data = new PoisonBlockData(hp, row, column);
        //data.position = transform.position;
        return data;
    }
}
