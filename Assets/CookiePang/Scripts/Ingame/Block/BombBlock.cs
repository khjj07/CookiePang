using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBlock : Block
{

    protected override void Start()
    {
        base.Start();
        _textMeshPro.gameObject.SetActive(false);
    }


    public override void Hit(int damage)
    {
        SoundManager.instance.PlaySound(1, "BombSound");
        GameManager.instance.Explode(this);
    }

    public override void Shock(int damage)
    {
        SoundManager.instance.PlaySound(1, "BombSound");
        GameManager.instance.Explode(this);
    }

    public override BlockData ToData(int row, int column)
    {
        BombBlockData data = new BombBlockData(hp, row, column);
        //data.position = transform.position;
        return data;
    }
}
