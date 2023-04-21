using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DefaultBlock : Block
{
    public override void Hit(int damage)
    {
        hp -= damage;
        transform.DORewind();
        if (hp <= 0)
        {
            EffectManager.instance.PlayEffect(1, this);
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
            EffectManager.instance.PlayEffect(0, this);
            transform.DOShakeScale(0.3f, 10);
            transform.DOShakePosition(0.3f, 10);
        }
    }
}