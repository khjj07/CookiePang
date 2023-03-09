using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Block : MonoBehaviour
{
    [Range(1, 10000)]
    public int hp;

    private TextMeshPro _textMeshPro;

    protected virtual void Start()
    {
        _textMeshPro = GetComponentInChildren<TextMeshPro>();
    }

    protected virtual void Update()
    {
        _textMeshPro.SetText(hp.ToString());
    }
    public virtual void OnCollsionEnter(Collision collision)
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
    public virtual BlockData ToData()
    {
        BlockData data = new BlockData();
        //data.position = transform.position;
        data.hp = this.hp;
        data.type = BlockType.DEFAULT;
        return data;
    }
}
