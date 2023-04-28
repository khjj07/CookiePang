using System;
using System.Collections.Generic;
using UnityEngine;

public class HeartManager : Singleton<HeartManager>
{

    private const string HEART_KEY = "HEART";
    private const string LAST_RECHARGE_TIME_KEY = "LAST_RECHARGE";

    public int maxHeart = 5;  //�ִ� ��Ʈ ����
    public int currentHeart;  //���� ��Ʈ ����
    private float rechargeInterval = 10f * 60f;  //��Ʈ �ڵ� ���� �ֱ� (�ʴ���)
    public TimeSpan timeSinceLastRecharge;
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }


    private void Start()
    {
        currentHeart = PlayerPrefs.GetInt(HEART_KEY, maxHeart);
    }

    private void Update()
    {
        Debug.Log(timeSinceLastRecharge.TotalSeconds);
        if (currentHeart < maxHeart)
        {
            // ������ ��Ʈ ���� �ð��� ���� �ð��� ���Ѵ�.
            DateTime lastRechargeTime = DateTime.Parse(PlayerPrefs.GetString(LAST_RECHARGE_TIME_KEY, DateTime.Now.ToString()));
            timeSinceLastRecharge = DateTime.Now - lastRechargeTime;

            // ���� �ð��� �Ǿ��ٸ� ��Ʈ�� �ڵ����� �����Ѵ�.
            if (timeSinceLastRecharge.TotalSeconds >= rechargeInterval)
            {
                int amountToAdd = Mathf.FloorToInt((float)timeSinceLastRecharge.TotalSeconds / rechargeInterval);
                currentHeart = Mathf.Min(currentHeart + amountToAdd, maxHeart);
                PlayerPrefs.SetInt(HEART_KEY, currentHeart);

                // ������ ��Ʈ ���� �ð��� �����Ѵ�.
                PlayerPrefs.SetString(LAST_RECHARGE_TIME_KEY, DateTime.Now.ToString());
            }
        }
    }

    public bool UseHeartNoReset()
    {
        if (currentHeart > 0)
        {
            currentHeart--;
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