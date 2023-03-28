using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonBlock : Block
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Ball"))
        {
            GameManager.instance.GameOver();
        }

    }
    public override BlockData ToData(int row, int column)
    {
        PoisonBlockData data = new PoisonBlockData(hp, row, column);
        //data.position = transform.position;
        return data;
    }
}
