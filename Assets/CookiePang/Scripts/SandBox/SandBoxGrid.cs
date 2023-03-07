using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SandBoxGrid : MonoBehaviour, IPointerEnterHandler
{
    public BlockType targetType=BlockType.NONE;
    public Block target = null;

    public void OnPointerEnter(PointerEventData eventData)
    {
        SandBoxUI.instance.current = this;
    }
}
