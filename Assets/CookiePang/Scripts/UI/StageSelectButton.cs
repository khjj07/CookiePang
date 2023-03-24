using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements.Experimental;
using UnityEngine.UIElements;

public class StageSelectButton : Button
{
    public int myIndex;

    public override void OnPointerClick(PointerEventData pointerEventData)
    {
        base.OnPointerClick(pointerEventData);
        StageManager.instance.SetCurrentByName("stage" + myIndex);

    }
    
}
