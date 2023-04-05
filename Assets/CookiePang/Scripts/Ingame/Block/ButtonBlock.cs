using System.Collections;
using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class ButtonBlock : Block
{
    public bool pressed = false;

    protected override void Update()
    { 
    }
    public override void Hit(int damage)
    {
        if(!pressed)
        {
            _textMeshPro.SetText("ON");
            pressed = true;
        }
        else if (pressed)
        {
            _textMeshPro.SetText("OFF");
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