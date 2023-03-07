using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SandBoxGrid : MonoBehaviour, IPointerEnterHandler , IPointerExitHandler
{
    public BlockType targetType=BlockType.NONE;
    public Block target = null;

    public void OnPointerEnter(PointerEventData eventData)
    {
        SandBoxUI.instance.current = this;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (SandBoxUI.instance.current == this)
        {
            SandBoxUI.instance.current = null;
        }
    }
}
