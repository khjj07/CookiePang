using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TitleScene : MonoBehaviour
{
    [SerializeField] private Slider[] slider;
    public Sprite[] sprites;
    public GameObject onOffButton;
    //private bool DontDestroy = true; 
    private void Awake()
    {

        //if (DontDestroy)
        //    DontDestroyOnLoad(this.gameObject);
    }
    private void Start()
    {
        slider[0].value = SoundManager.instance.Player[0].Volume;
        slider[1].value = SoundManager.instance.Player[1].Volume;
        if (SoundManager.instance.isStartSound) 
        { 
            SoundManager.instance.PlaySound(0, "MainSound"); 
            SoundManager.instance.isStartSound = false; 
        }
        if (EffectManager.instance.isParticle == false)
            onOffButton.GetComponent<Image>().sprite = sprites[1];
        else
            onOffButton.GetComponent<Image>().sprite = sprites[0];
    }
    
}
