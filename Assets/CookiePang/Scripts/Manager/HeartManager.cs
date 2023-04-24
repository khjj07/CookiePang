using System;
using UnityEngine;

public class HeartManager : MonoBehaviour
{
    public static HeartManager instance;

    private const string HEART_KEY = "HEART";
    private const string LAST_RECHARGE_TIME_KEY = "LAST_RECHARGE_TIME";

    private int maxHeart = 5;  //�ִ� ��Ʈ ����
    private int currentHeart;  //���� ��Ʈ ����
    private float rechargeInterval = 10f * 60f;  //��Ʈ �ڵ� ���� �ֱ� (�ʴ���)

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        currentHeart = PlayerPrefs.GetInt(HEART_KEY, maxHeart);
    }

    private void Update()
    {
        if (currentHeart < maxHeart)
        {
            // ������ ��Ʈ ���� �ð��� ���� �ð��� ���Ѵ�.
            DateTime lastRechargeTime = DateTime.Parse(PlayerPrefs.GetString(LAST_RECHARGE_TIME_KEY, DateTime.Now.ToString()));
            TimeSpan timeSinceLastRecharge = DateTime.Now - lastRechargeTime;

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
}