using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class SettingButton : Button
{
    [SerializeField] private GameObject settingPanel;

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
            EffectManager.instance.isParticle = false;
        else
            EffectManager.instance.isParticle = true;
    }
}
