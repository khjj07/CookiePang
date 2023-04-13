using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarSlider : MonoBehaviour
{
    public Star[] stars = new Star[3];
    public float offset;
    public RectTransform filledArea;
    public void SetStar(int index, int value)
    {
        //stars[index].GetComponent<RectTransform>().anchoredPosition = new Vector2(offset * value, 0);
        stars[index].SetValue(value);
    }

    public void SetFillArea(int num)
    {
        filledArea.sizeDelta = new Vector2(800-offset*num,0);
    }

}
