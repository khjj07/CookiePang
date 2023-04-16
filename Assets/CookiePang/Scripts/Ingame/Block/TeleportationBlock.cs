using System.Collections;
using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportationBlock : Block
{
    public TeleportationBlock destination;
    private bool _ballEntered = false;

    protected override void Start()
    {
        base.Start();
        foreach (var block in GameManager.instance.blocks)
        {
            if (block != this && block as TeleportationBlock && hp == block.hp)
            {
                destination = (TeleportationBlock)block;
                break;
            }
        }
        if (!SceneManager.GetActiveScene().name.Equals("SandBox"))
            _textMeshPro.gameObject.SetActive(false);

        this.UpdateAsObservable()
            .Where(_ => GameManager.instance.ball.isFloor)
            .Subscribe(_ => { _ballEntered = false; });
    }
    public override void Hit(int damage)
    {
        if (!_ballEntered)
        {
            if (destination)
            {
                _ballEntered = true;
                destination._ballEntered = true;
                GameManager.instance.ball.transform.position = destination.transform.position;
                SoundManager.instance.PlaySound(1, "TeleportSound");
                EffectManager.instance.PlayEffect(3, this);
                EffectManager.instance.PlayEffect(3, destination);
            }
        }
    }

    public override BlockData ToData(int row, int column)
    {
        Vector2Int index =GameManager.instance.GetBlockIndex(destination);
        TeleportationBlockData data = new TeleportationBlockData(hp, row, column,index.x,index.y);
        //data.position = transform.position;
        return data;
    }
    public override void GetData(BlockData data)
    {
        hp = data.hp;
        TeleportationBlockData tel = data as TeleportationBlockData;
        if (tel != null && GameManager.instance.blocks[tel.destinationRow, tel.destinationCol])
        {
            var telInstance2 = GameManager.instance.blocks[tel.destinationRow, tel.destinationCol] as TeleportationBlock;
            destination = telInstance2;
            telInstance2.destination = this;

        }
    }
}