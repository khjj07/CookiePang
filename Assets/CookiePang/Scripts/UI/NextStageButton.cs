using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class NextStageButton : Button
{
    // Start is called before the first frame update
    public override void OnPointerClick(PointerEventData pointerEventData)
    {
        base.OnPointerClick(pointerEventData);
        StageManager.instance.SetCurrent(++StageManager.instance.currentIndex);
        SceneFlowManager.ChangeScene("Stage");
    }
}
