using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public bool isFloor=true;
    public int damage=1;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        collider.GetComponent<Block>().Hit(damage);
    }
        private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Floor"))
        {
            SoundManager.instance.PlaySound(1, "BallCrushSound"); 
            GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            isFloor = true;
        }

        if(collision.collider.CompareTag("Block"))
        {
            if(collision.collider.gameObject.name != "JellyBlock(Clone)")
            {
                SoundManager.instance.PlaySound(1, "BallCrushSound");
            }
            collision.collider.GetComponent<Block>().Hit(damage);
        }

        if (collision.collider.CompareTag("Wall"))
        {
            SoundManager.instance.PlaySound(1, "BallCrushSound");
        }
    }

    public void Shoot(Vector3 shootForce)
    {
        GetComponent<Rigidbody2D>().AddForce(shootForce, ForceMode2D.Impulse);
        isFloor = false;
    }
}
