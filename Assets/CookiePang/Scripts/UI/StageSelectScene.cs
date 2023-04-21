using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StageSelectScene : MonoBehaviour
{
    [SerializeField] private Slider[] slider;
    public Text onOffParticleText;
    private void Start()
    {
        slider[0].value = SoundManager.instance.Player[0].Volume;
        slider[1].value = SoundManager.instance.Player[1].Volume;

        if (EffectManager.instance.isParticle == false)
            onOffParticleText.text = "OFF";
        else
            onOffParticleText.text = "ON";


    }
}
