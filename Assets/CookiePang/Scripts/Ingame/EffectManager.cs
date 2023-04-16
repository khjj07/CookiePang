using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EffectManager : Singleton<EffectManager>
{
    public List<GameObject> effectList = new List<GameObject>();
    //0 ��Ű�� �浹
    //1 ��Ű�� ����
    //2 ��ź�� ����
    //3 �ڷ���Ʈ �浹
    //4 ĵ��� �浹
    //5 ���ع��� ����
    public void PlayEffect(int num,Block pos)
    {
        GameObject effect = Instantiate(effectList[num], pos.transform.position, pos.transform.rotation);
        Destroy(effect, 2);
    }
}
