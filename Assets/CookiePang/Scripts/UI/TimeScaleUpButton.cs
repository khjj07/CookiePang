using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class TimeScaleUpButton : Button
{
    private void Awake()
    {
        GetComponentInChildren<Text>().text = "Speed x1";
    }
    public override void OnPointerClick(PointerEventData pointerEventData)
    {
        base.OnPointerClick(pointerEventData);
        if (!GameManager.instance.isTimeScaleUp)
        {
            GameManager.instance.isTimeScaleUp = true;
            GetComponentInChildren<Text>().text = "Speed x3";
        }
        else
        {
            GameManager.instance.isTimeScaleUp = false;
            GetComponentInChildren<Text>().text = "Speed x1";
        }
        
    }
}
