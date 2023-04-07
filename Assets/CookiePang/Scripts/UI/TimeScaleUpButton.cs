using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class TimeScaleUpButton : Button
{
    public override void OnPointerClick(PointerEventData pointerEventData)
    {
        base.OnPointerClick(pointerEventData);
        if (!GameManager.instance.isTimeScaleUp)
        {
            GameManager.instance.isTimeScaleUp = true;
        }
        else
        {
            GameManager.instance.isTimeScaleUp = false;
        }
        
    }
}
