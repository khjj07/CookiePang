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
            //SoundManager_2.instance.BallSound();
            isFloor = true;
            GetComponent<Rigidbody>().velocity = Vector3.zero; 
        }
        if(collision.collider.CompareTag("Block"))
        {
            //SoundManager_2.instance.BlockHitSound();
            collision.collider.GetComponent<Block>().Hit(damage);
        }
        if (collision.collider.CompareTag("Wall"))
        {
            //SoundManager_2.instance.BallSound();
        }
    }

    public void Shoot(Vector3 shootForce)
    {
        GetComponent<Rigidbody>().AddForce(shootForce, ForceMode.Impulse);
        isFloor = false;
    }
}
