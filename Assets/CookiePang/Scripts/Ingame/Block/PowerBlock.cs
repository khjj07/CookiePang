using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerBlock : Block
{
    protected virtual void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ball"))
        {
            collision.gameObject.GetComponent<Ball>().damage += 1;
            //SoundManager_2.instance.BlockDieSound();
        }
        
    }

    public override BlockData ToData(int row, int column)
    {
        PowerBlockData data = new PowerBlockData(hp, row, column);
        return data;
    }

}
