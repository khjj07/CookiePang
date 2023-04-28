using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MacaroonBlock : Block
{
    public override BlockData ToData(int row, int column)
    {
        MacaroonBlockData data = new MacaroonBlockData(hp,row, column);
        //data.position = transform.position;
        return data;
    }

    public override void Hit(int damage)
    {
        hp -= damage;
        transform.DORewind();
        if (hp <= 0)
        {
            EffectManager.instance.PlayEffect(1, transform.gameObject, 2f);
            _textMeshPro.enabled = false;
            GetComponent<Collider2D>().enabled = false;
            SoundManager.instance.PlaySound(1, "CookieBreak1Sound");
            transform.DOScale(Vector3.zero, 0.1f).OnComplete(() =>
            {
                GameManager.instance.DeleteBlock(this);
            });
        }
        else
        {
            transform.DOShakeScale(0.3f, 10);
            transform.DOShakePosition(0.3f, 10);
        }
    }
}
