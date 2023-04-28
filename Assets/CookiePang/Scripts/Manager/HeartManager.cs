using System;
using System.Collections.Generic;
using UnityEngine;

public class HeartManager : Singleton<HeartManager>
{

    private const string HEART_KEY = "HEART";
    private const string LAST_RECHARGE_TIME_KEY = "LAST_RECHARGE";

    public int maxHeart = 5;  //최대 하트 개수
    public int currentHeart;  //현재 하트 개수
    private float rechargeInterval = 10f * 60f;  //하트 자동 충전 주기 (초단위)
    public int addMobCount = 2;
    public bool isAdd = true;
    public TimeSpan timeSinceLastRecharge;
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    
    }

    private void Start()
    {
        currentHeart = PlayerPrefs.GetInt(HEART_KEY, maxHeart);
        var lastRechargeTime = DateTime.Parse(PlayerPrefs.GetString(LAST_RECHARGE_TIME_KEY, DateTime.Now.ToString()));
        if ((lastRechargeTime - DateTime.Now).TotalSeconds < 10.0f);
        {
            PlayerPrefs.SetString(LAST_RECHARGE_TIME_KEY, DateTime.Now.ToString());
        }
    }

    private void Update()
    {
        if (currentHeart < maxHeart)
        {
            // 마지막 하트 충전 시간이 지난 시간을 구한다.
            DateTime lastRechargeTime = DateTime.Parse(PlayerPrefs.GetString(LAST_RECHARGE_TIME_KEY, DateTime.Now.ToString()));
            timeSinceLastRecharge = DateTime.Now - lastRechargeTime;

            // 충전 시간이 되었다면 하트를 자동으로 충전한다.
            if (timeSinceLastRecharge.TotalSeconds >= rechargeInterval)
            {
                int amountToAdd = Mathf.FloorToInt((float)timeSinceLastRecharge.TotalSeconds / rechargeInterval);
                currentHeart = Mathf.Min(currentHeart + amountToAdd, maxHeart);
                PlayerPrefs.SetInt(HEART_KEY, currentHeart);

                // 마지막 하트 충전 시간을 갱신한다.
                PlayerPrefs.SetString(LAST_RECHARGE_TIME_KEY, DateTime.Now.ToString());
            }
        }
    }

    public bool UseHeartNoReset()
    {
        if (currentHeart > 0)
        {
            currentHeart--;
            addMobCount--;
            PlayerPrefs.SetInt(HEART_KEY, currentHeart);
            return true;
        }
        else
        {
            return false;
        }
    }

    public void AddHeartNoReset(int amount)
    {
        currentHeart = Mathf.Min(currentHeart + amount, maxHeart);
        PlayerPrefs.SetInt(HEART_KEY, currentHeart);
    }

    public bool UseHeart()
    {
        if (currentHeart > 0)
        {
            currentHeart--;
            PlayerPrefs.SetInt(HEART_KEY, currentHeart);
            PlayerPrefs.SetString(LAST_RECHARGE_TIME_KEY, DateTime.Now.ToString());
            return true;
        }
        else
        {
            return false;
        }
    }

    public void AddHeart(int amount)
    {
        currentHeart = Mathf.Min(currentHeart + amount, maxHeart);
        PlayerPrefs.SetInt(HEART_KEY, currentHeart);
        PlayerPrefs.SetString(LAST_RECHARGE_TIME_KEY, DateTime.Now.ToString());
    }

    public float GetRechargingTime()
    {
       return rechargeInterval - (float)timeSinceLastRecharge.TotalSeconds;
    }
}