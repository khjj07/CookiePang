using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Pool;
using System.Threading.Tasks;

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

    public List<ObjectPool<GameObject>> effectPools = new List<ObjectPool<GameObject>>();

    private bool DontDestroy = true;
    public bool isParticle = true;
    private int effectIndex = 0;
    private void Awake()
    {
        if (DontDestroy)
            DontDestroyOnLoad(this.gameObject);
        foreach (var e in effectList)
            effectPools.Add(new ObjectPool<GameObject>(LoadEffectInListStep, OnGet, OnRelease, OnDestory, maxSize: 5));
    }
    public GameObject LoadEffectInListStep()
    {
        GameObject effect = Instantiate(effectList[effectIndex].effect);
        return effect;
    }
    private void OnGet(GameObject obj)
    {
        obj.SetActive(true);
        obj.GetComponent<ParticleSystem>().Play();
    }

    public void PlayEffect(int index,GameObject obj, float time)
    {
        if (!isParticle)
            return;
        effectIndex=index;
        var effect = effectPools[index].Get();
        effect.transform.position = obj.transform.position;
        effect.transform.rotation = obj.transform.rotation;
        Task.Run(() =>
        {
            Task.Delay((int)time * 1000);
            effectPools[index].Release(effect);
        });
    }

    //=> Release가 실행될 때 실행되는 함수 element가 실행함.
    private void OnRelease(GameObject obj)
    {
        obj.SetActive(false);
    }

    private void OnDestory(GameObject obj)
    {
        Destroy(obj);
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
