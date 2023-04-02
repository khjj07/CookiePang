using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TitleSceneSound : MonoBehaviour
{
    [SerializeField] private Slider[] slider;
    private void Awake()
    {
    }
    private void Start()
    {
        slider[0].value = SoundManager.instance.Player[0].Volume;
        slider[1].value = SoundManager.instance.Player[1].Volume;
        SoundManager.instance.PlaySound(0, "MainSound");
    }

}
