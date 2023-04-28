using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class HeartUI : MonoBehaviour
{
    TextMeshProUGUI heartNumber;
    TextMeshProUGUI rechargingTime;
    // Start is called before the first frame update
    void Start()
    {
        heartNumber = transform.Find("heartNumber").GetComponent<TextMeshProUGUI>();
        rechargingTime = transform.Find("rechargingTime").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        heartNumber.text = HeartManager.instance.currentHeart.ToString();
        if(HeartManager.instance.currentHeart<5)
        {
            int minute = (int)HeartManager.instance.GetRechargingTime() / 60;
            int second = (int)HeartManager.instance.GetRechargingTime()-minute*60;
            if(second>9)
            {
                 rechargingTime.text = minute + " : " + second;
            }
            else
            {
                rechargingTime.text = minute + " : 0" + second;
            }
        }
        else
        {
            rechargingTime.text = "Full";
        }
        
    }
}
