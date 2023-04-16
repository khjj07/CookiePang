using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EffectManager : Singleton<EffectManager>
{
    public List<GameObject> effectList = new List<GameObject>();
    //0 쿠키블럭 충돌
    //1 쿠키블럭 삭제
    //2 폭탄블럭 삭제
    //3 텔레포트 충돌
    //4 캔디블럭 충돌
    //5 독극물블럭 삭제
    public void PlayEffect(int num,Block pos)
    {
        GameObject effect = Instantiate(effectList[num], pos.transform.position, pos.transform.rotation);
        Destroy(effect, 2);
    }
}
