using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonBlock : Block
{
    protected override void Start()
    {
        base.Start();
        _textMeshPro.gameObject.SetActive(false);
    }

    protected override void Update()
    {
       
    }

    public override void Hit(int damage)
    {
        GameManager.instance.DeleteBlock(this);
        GameManager.instance.ball.isFloor = false;
        if (GameManager.instance.ballCount > 0)
        {
            GameManager.instance.ballCount--;
        }
        SoundManager.instance.PlaySound(1, "PoisonBlockSound");
        EffectManager.instance.PlayEffect(5, transform.gameObject, 4f); //블럭에서 나오는 파티클
        EffectManager.instance.UiEffect(1, GameManager.instance.ballCntTxt.transform.position);
        GameManager.instance.DeadLineCount();
        GameManager.instance.StarsCountImage();
    }

    public override void Shock(int damage)
    {
        Hit(damage);
    }

    public override BlockData ToData(int row, int column)
    {
        PoisonBlockData data = new PoisonBlockData(hp, row, column);
        //data.position = transform.position;
        return data;
    }
}
