using System.Collections;
using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonBlock : Block
{
    public bool pressed = false;

    public override void Hit(int damage)
    {
        if(!pressed)
        {
            pressed = true;
        }
        else if ( pressed)
        {
            pressed = false;
        }
    }


    public override BlockData ToData(int row, int column)
    {
        ButtonBlockData data = new ButtonBlockData(row, column);
        //data.position = transform.position;
        return data;
    }
    public override void GetData(BlockData data)
    {
        ButtonBlockData tel = data as ButtonBlockData;
    }
}