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
        _textMeshPro.SetText(hp.ToString());
    }

    protected void OnCollisionEnter(Collision collision)
    {
        hp -= collision.collider.GetComponent<Ball>().damage;
        if(hp<=0)
        {
            GameManager.instance.DeleteBlock(this);
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
