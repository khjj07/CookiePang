using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class EffectStruct
{
    public string name;
    public GameObject effect;
}
[Serializable]
public class UIStruct
{
    public string name;
    public GameObject effect;
}
public class EffectManager : Singleton<EffectManager>
{
    public List<EffectStruct> effectList = new List<EffectStruct>();
    public List<UIStruct> uiList = new List<UIStruct>();
    private bool DontDestroy = true;
    public bool isParticle = true;
    private void Awake()
    {
        if (DontDestroy)
            DontDestroyOnLoad(this.gameObject);

    }
    public void PlayEffect(int num,GameObject pos, float time)
    {
        if (!isParticle)
            return;
        GameObject effect = Instantiate(effectList[num].effect, pos.transform.position, pos.transform.rotation);
        Destroy(effect, time);
    }
    public void UiEffect(int num, Vector3 pos)
    {
        if (!isParticle)
            return;
        GameObject effect = Instantiate(uiList[num].effect, pos, Quaternion.Euler(pos));
        
        effect.SetActive(false);
        effect.SetActive(true);
        
        Destroy(effect, 2f);
    }
    
}
