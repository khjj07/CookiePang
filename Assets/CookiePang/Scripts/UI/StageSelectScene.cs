using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StageSelectScene : MonoBehaviour
{
    [SerializeField] private Slider[] slider;
    public Sprite[] sprites;
    public GameObject onOffButton;
    private void Start()
    {
        slider[0].value = SoundManager.instance.Player[0].Volume;
        slider[1].value = SoundManager.instance.Player[1].Volume;

        if (EffectManager.instance.isParticle == false)
            onOffButton.GetComponent<Image>().sprite = sprites[1];
        else
            onOffButton.GetComponent<Image>().sprite = sprites[0];


    }
}
