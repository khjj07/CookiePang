using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class TimeScaleUpButton : MonoBehaviour
{
    public Sprite[] sprites;
    public void TimeScaleButtonClick()
    {
        if (!GameManager.instance.isTimeScaleUp)
        {
            GameManager.instance.isTimeScaleUp = true;
            this.GetComponent<Image>().sprite = sprites[0];
        }
        else
        {
            GameManager.instance.isTimeScaleUp = false;
            this.GetComponent<Image>().sprite = sprites[1];
        }
        
    }
}
