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
            GameManager.instance.FailaddMob.ShowAd();//���� �����ؾ� �� �� �Լ�
            HeartManager.instance.addMobCount = 2;
        }
        else
        {
            StageManager.instance.SetCurrent(++StageManager.instance.currentIndex); //���� �������� �������� �Ѿ
            SceneFlowManager.ChangeScene("Stage");
            Time.timeScale = 1;
        }


    }
}
