using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public int damage=1;
    public bool isFloor=true;
    private float ballTimeScale = 10f; //TimeScale ������ ����
    private float currentBallTimeScale = 10f; //TimeScale �ʱ�ȭ ����
    public bool isTimeScale = false; //TimeScale Ȯ�κ���
    public Vector3 defaultBallPos; //ó�� ����ġ
    public Vector3 currentBallPos; //���������� ���� ����ġ
    private void Start()
    {
        defaultBallPos = transform.position;
        currentBallPos = transform.position;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Floor"))
        {
            currentBallPos = transform.position; //����ġ ������Ʈ
            SoundManager.instance.PlaySound(1, "BallCrushSound");
            isFloor = true;
            GetComponent<Rigidbody>().velocity = Vector3.zero;

            //Ŭ����,���ӿ��� �ڵ��߰�
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
    //�ӽ��ڵ�
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
        IsTimeScale(); //���� ������ �������� ���������� ��������
    }
    private void IsTimeScale()
    {
        if (!isFloor) //���� ������ ������
        {
            ballTimeScale -= Time.deltaTime;
            if (ballTimeScale <= 0)
            {
                isTimeScale = true;
            }

        }
        else
        {
            ballTimeScale = currentBallTimeScale; //10�ʷ� �ʱ�ȭ
            isTimeScale = false;
        }
    }
}
