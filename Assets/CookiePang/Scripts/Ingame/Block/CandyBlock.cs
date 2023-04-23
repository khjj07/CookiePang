using System.Collections;
using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CandyBlock : Block
{
    public bool holeIn = false;

    protected override void Update()
    {
        //_textMeshPro.SetText(hp.ToString());
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ball") && !holeIn)
        {
            EffectManager.instance.PlayEffect(4, transform.gameObject, 2f);
            SoundManager.instance.PlaySound(1, "CandyBlockSound");
            GameManager.instance.DeleteBlock(this);
        }
    }

    public override BlockData ToData(int row, int column)
    {
        CandyBlockData data = new CandyBlockData(row, column);
        //data.position = transform.position;
        return data;
    }
}