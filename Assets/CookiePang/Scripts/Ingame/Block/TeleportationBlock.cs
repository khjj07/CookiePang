using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportationBlock : Block
{
    public TeleportationBlock destination;
    private bool _ballEntered = false;

    protected override void Start()
    {
        base.Start();
        foreach(var block in GameManager.instance.blocks)
        {
            if( block != this && block as TeleportationBlock && hp == block.hp)
            {
                destination = (TeleportationBlock)block;
                break;
            }
        }
        if(!SceneManager.GetActiveScene().name.Equals("SandBox"))
            _textMeshPro.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball") && !_ballEntered)
        {
            if(destination)
            {
                destination._ballEntered = true;
                other.transform.position = destination.transform.position;
                SoundManager.instance.PlaySound(1, "TeleportSound");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ball") && _ballEntered)
        {
            _ballEntered = false;
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