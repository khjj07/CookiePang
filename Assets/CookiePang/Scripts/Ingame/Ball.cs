using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public int damage=1;
    public bool isFloor=true;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Floor"))
        {
            SoundManager.instance.PlaySound(1, "BallCrushSound");
            isFloor = true;
            GetComponent<Rigidbody>().velocity = Vector3.zero;

            if (GameManager.instance.isClear) 
            { 
                SoundManager.instance.PlaySound(1, "StageClearSound");
                GameManager.instance.StageClear();
            }
            else if (GameManager.instance.ballCount <= 0) 
            {
                SoundManager.instance.PlaySound(1, "StageFailSound");
                GameManager.instance.GameOver();
            }
        }
        if(collision.collider.CompareTag("Block"))
        {
            SoundManager.instance.PlaySound(1, "BallCrushSound");
            collision.collider.GetComponent<Block>().Hit(damage);
        }
        if (collision.collider.CompareTag("Wall"))
        {
            SoundManager.instance.PlaySound(1, "BallCrushSound");
        }
    }

    public void Shoot(Vector3 shootForce)
    {
        GetComponent<Rigidbody>().AddForce(shootForce, ForceMode.Impulse);
        isFloor = false;
    }
}
