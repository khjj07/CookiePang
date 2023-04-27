using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class TimeScaleUpButton : Button
{
    public Sprite[] sprites;
    public override void OnPointerClick(PointerEventData pointerEventData)
    {
        base.OnPointerClick(pointerEventData);
        if (!GameManager.instance.isTimeScaleUp)
        {
            GameManager.instance.isTimeScaleUp = true;
            this.GetComponent<Image>().sprite = sprites[0];
        }
        else
        {
            GameManager.instance.isTimeScaleUp = false;
            this.GetComponent<Image>().sprite = sprites[1];
        }
        
    }
}
