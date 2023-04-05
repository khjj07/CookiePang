using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TitleSceneSound : MonoBehaviour
{
    [SerializeField] private Slider[] slider;

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
        
    }
    
}
