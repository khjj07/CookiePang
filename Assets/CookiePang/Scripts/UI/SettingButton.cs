using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SettingButton : Button
{
    [SerializeField] private GameObject settingPanel;
    public Text onOffText;
    public override void OnPointerClick(PointerEventData pointerEventData)
    {
        base.OnPointerClick(pointerEventData);
        ClickSound();
    }
    public void OpenPanelButton()
    {
        settingPanel.SetActive(true);
    }
    public void ExitPanelButton()
    {
        settingPanel.SetActive(false);
    }
    public void OnOffParticleButton()
    {
        ClickSound();
        if (EffectManager.instance.isParticle)
        {
            EffectManager.instance.isParticle = false;
            onOffText.text = "OFF";
        }
        else
        {
            EffectManager.instance.isParticle = true;
            onOffText.text = "ON";
        }
            
    }
    public void ClickSound()
    {
        SoundManager.instance.PlaySound(1, "UiClickSound");
    }
}
