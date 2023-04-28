using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellyBlock : Block
{

    protected override void Start()
    {
        hp = 1;
    }

    protected override void Update()
    {
      
    }

    public override void Hit(int damage)
    {
        transform.DORewind();
        transform.DOShakeScale(0.3f, 10);
        transform.DOShakePosition(0.3f, 10);
        SoundManager.instance.PlaySound(1, "JellyBlockSound");
    }
    public override BlockData ToData(int row, int column)
    {
        JellyBlockData data = new JellyBlockData(hp, row, column);
        return data;
    }
}
