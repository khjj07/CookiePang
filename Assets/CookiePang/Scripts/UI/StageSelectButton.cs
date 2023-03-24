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
        if(GameManager.instance != null)
        {
            if (GameManager.instance.isClear)
            {
                StageManager.instance.currentIndex++;
                StageManager.instance.SetCurrentByName("stage" + StageManager.instance.currentIndex);
            }
            else
            {
                StageManager.instance.SetCurrentByName("stage" + myIndex);
            }
        }
        else
        {
            StageManager.instance.currentIndex = myIndex;
            StageManager.instance.SetCurrentByName("stage" + myIndex);
        }
        
    }

    
}
