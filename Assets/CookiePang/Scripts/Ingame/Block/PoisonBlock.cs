using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonBlock : Block
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ball"))
        {
            GameManager.instance.GameOver();
            GameManager.instance.ball.transform.position = GameManager.instance.ball.defaultBallPos;
            GameManager.instance.ball.isFloor = false;
            GameManager.instance.ball.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }

    }
    public override BlockData ToData(int row, int column)
    {
        PoisonBlockData data = new PoisonBlockData(hp, row, column);
        //data.position = transform.position;
        return data;
    }
}
