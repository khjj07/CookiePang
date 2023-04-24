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
    public Sprite[] sprite;
    protected override void Update()
    { 
    }
    public override void Hit(int damage)
    {
        if(!pressed)
        {
            _textMeshPro.SetText("ON");
            GetComponent<SpriteRenderer>().sprite = sprite[1];
            SoundManager.instance.PlaySound(1, "ButtonBlockOnSound");
            pressed = true;
        }
        else if (pressed)
        {
            _textMeshPro.SetText("OFF");
            GetComponent<SpriteRenderer>().sprite = sprite[0];
            SoundManager.instance.PlaySound(1, "ButtonBlockOffSound");
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