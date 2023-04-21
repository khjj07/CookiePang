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
}
