using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

[ExecuteAlways]
public class Block : MonoBehaviour
{
    [Range(1, 10000)]
    public int hp;
    public BlockType type;

    private TextMeshPro _textMeshPro;

    protected void Start()
    {
        _textMeshPro=GetComponentInChildren<TextMeshPro>();
    }

    protected void Update()
    {
        if (type == BlockType.TELEPORT || type == BlockType.BOMB)
            return;
        _textMeshPro.SetText(hp.ToString());
    }

    protected void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            hp -= collision.collider.GetComponent<Ball>().damage;
            if (hp <= 0)
            {
                GameManager.instance.DeleteBlock(this);
            }
        }
       
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BombBlock"))
        {
            hp -= other.gameObject.GetComponent<BombBlock>().damage;
            if (hp <= 0)
            {
                GameManager.instance.DeleteBlock(this);
            }
        }
    }
    public BlockData ToData()
    {
        BlockData data = new BlockData();
        data.position = transform.position;
        data.hp = this.hp;
        data.type = this.type;
        return data;
    }
}
