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

            //클리어,게임오버 코드추가
            IsClear(); 
            if (GameManager.instance.isClear) 
            {
                GameManager.instance.StageClear(); 
            }
            else if (GameManager.instance.ballCount <= 0) 
            {
                GameManager.instance.GameOver();
            }
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
        
        GetComponent<Rigidbody>().AddForce(shootForce, ForceMode.Impulse);
        isFloor = false;
    }
    //임시코드
    private void IsClear()
    {
        DefaultBlock defaultBlock = FindObjectOfType<DefaultBlock>();
        
        if (defaultBlock == null)
        {
            GameManager.instance.isClear = true;
        }
        
    }

}
