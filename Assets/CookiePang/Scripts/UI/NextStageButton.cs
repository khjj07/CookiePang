using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using GoogleMobileAds.Api;
using GoogleMobileAds;
public class NextStageButton : MonoBehaviour
{

    public void NextStageClick()
    {
        if (HeartManager.instance.addMobCount <= 0)
        {
            GameManager.instance.FailaddMob.ShowAd();//광고를 시작해야 할 때 함수
            HeartManager.instance.addMobCount = 2;
        }
        else
        {
            StageManager.instance.SetCurrent(++StageManager.instance.currentIndex); //광고가 닫혔을때 스테이지 넘어감
            SceneFlowManager.ChangeScene("Stage");
            Time.timeScale = 1;
        }


    }
}
