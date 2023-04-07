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
      
        if (hp <= 0)
        {
            _textMeshPro.enabled = false;
            GetComponent<Collider2D>().enabled = false;
            SoundManager.instance.PlaySound(1, "CookieBreak1Sound");
        }
        transform.DORewind();
        transform.DOShakeScale(0.3f, 10);
        transform.DOShakePosition(0.3f,10).OnComplete(() =>
        {
            transform.DOScale(Vector3.zero, 0.1f).OnComplete(() =>
            {
                if (hp <= 0)
                {
                    GameManager.instance.DeleteBlock(this);
                }
            });
        });
    }
}