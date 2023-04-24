using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements.Experimental;
using UnityEngine.UIElements;
using TMPro;

public class StageSelectButton : Button
{
    public bool isLocked;
    public int myIndex;
    private TextMeshProUGUI _text;

    public override void Start()
    {
        base.Start();
        _text = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void Update()
    {
        if (isLocked)
        {

        }
        _text.SetText(myIndex.ToString());
    }

    public override void OnPointerClick(PointerEventData pointerEventData)
    {
        base.OnPointerClick(pointerEventData);
        StageManager.instance.SetCurrent(myIndex);
        SceneFlowManager.ChangeScene("Stage");
    }
}
