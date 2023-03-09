using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SandBoxGrid : MonoBehaviour, IPointerEnterHandler , IPointerExitHandler
{
    public Block target = null;
    public int row;
    public int column;
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
