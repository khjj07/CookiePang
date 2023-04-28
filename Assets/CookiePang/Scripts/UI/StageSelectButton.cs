using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements.Experimental;
using UnityEngine.UI;
using TMPro;

public class StageSelectButton : Button
{
    public Sprite onSprite;
    public Sprite offSprite;
    public bool isLocked;
    public int achievementRate;
    public int myIndex;
    private TextMeshProUGUI _text;
    private Image _image;
    public GameObject[] stars = new GameObject[3];

    public override void Start()
    {
        base.Start();
        _text = GetComponentInChildren<TextMeshProUGUI>();
        _image = GetComponent<Image>();
        if (isLocked)
        {
            _image.sprite = offSprite;
        }
        else
        {
            _image.sprite = onSprite;
        }

        for (int i = 0; i < stars.Length; i++)
        {
            stars[i].SetActive(false);
        }

        for (int i = 0; i < achievementRate; i++)
        {
            stars[i].SetActive(true);
        }
    }

    public void Update()
    {
        _text.SetText(myIndex.ToString());
    }

    public override void OnPointerClick(PointerEventData pointerEventData)
    {
        if (!isLocked && HeartManager.instance.currentHeart>0)
        {
            base.OnPointerClick(pointerEventData);
            StageManager.instance.SetCurrent(myIndex);
            SceneFlowManager.ChangeScene("Stage");
        }
    }
}
