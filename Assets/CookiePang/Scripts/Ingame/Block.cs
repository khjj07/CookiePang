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

    protected TextMeshPro _textMeshPro;

    protected virtual void Start()
    {
        _textMeshPro = GetComponentInChildren<TextMeshPro>();
    }

    protected virtual void Update()
    {
        _textMeshPro.SetText(hp.ToString());
    }

    public virtual void Hit(int damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            GetComponent<Collider2D>().enabled = false;
            SoundManager.instance.PlaySound(1, "CookieBreak1Sound");
            GameManager.instance.DeleteBlock(this);
        }
    }

    public virtual void Shock(int damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            SoundManager.instance.PlaySound(1, "CookieBreak1Sound");
            GameManager.instance.DeleteBlock(this);
        }
    }

    public virtual BlockData ToData(int row, int column)
    {
        DefaultBlockData data = new DefaultBlockData(hp, row, column);
        //data.position = transform.position;
        return data;
    }

    public virtual void GetData(BlockData data)
    {
        hp = data.hp;
    }
}
