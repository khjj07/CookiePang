using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public int damage=1;
    public bool isFloor=true;
    private float ballTimeScale = 10f; //TimeScale 감소할 변수
    private float currentBallTimeScale = 10f; //TimeScale 초기화 변수
    public bool isTimeScale = false; //TimeScale 확인변수
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Floor"))
        {
            SoundManager.instance.PlaySound(1, "BallCrushSound");
            isFloor = true;
            GetComponent<Rigidbody2D>().velocity = Vector3.zero;

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
        
        GetComponent<Rigidbody2D>().AddForce(shootForce, ForceMode2D.Impulse);
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
    private void Update()
    {
        IsTimeScale(); //공이 땅에서 떠난이후 몇초지나면 빨라지게
    }
    private void IsTimeScale()
    {
        if (!isFloor) //공이 땅에서 떠나면
        {
            ballTimeScale -= Time.deltaTime;
            if (ballTimeScale <= 0)
            {
                isTimeScale = true;
            }

        }
        else
        {
            ballTimeScale = currentBallTimeScale; //10초로 초기화
            isTimeScale = false;
        }
    }
}
