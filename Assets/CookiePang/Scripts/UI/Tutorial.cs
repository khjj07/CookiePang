using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
[Serializable]
public class PanelStruct
{
    public string name;
    public GameObject panel;
    public Text text;
}
public class Tutorial : MonoBehaviour
{
    public List<PanelStruct> panelList = new List<PanelStruct>();

    void Start()
    {
        if(StageManager.instance.currentIndex == 1)
        {
            StartCoroutine(Turorial1Step1());
          
            
        }
    }
    void Update()
    {
        if (GameManager.instance.ball.isFloor && GameManager.instance.ballCount == GameManager.instance.ballCount-1)
        {
            StartCoroutine(Turorial1Step2());
        }
    }
    IEnumerator Turorial1Step1()
    {
        panelList[0].panel.SetActive(true);
        panelList[0].text.GetComponent<Text>().text = "간단하게 게임하는 법을 알려줄게요.";
        yield return new WaitForSeconds(2);
        panelList[0].text.GetComponent<Text>().text = "저희의 목표는 별사탕을 던져서 \n 모든 쿠키를 제거하는거예요";
        yield return new WaitForSeconds(4);
        panelList[0].panel.SetActive(false);
        panelList[1].panel.SetActive(true);
        yield return new WaitForSeconds(3);
        panelList[1].panel.SetActive(false);
        panelList[2].panel.SetActive(true);
        yield return new WaitForSeconds(3);
        panelList[2].panel.SetActive(false);
        panelList[0].panel.SetActive(true);
        panelList[0].text.GetComponent<Text>().text = "공은 아래의 슬라이더를 움직이거나 \n 화면을 직접 터치해서 던질 수 있어요";
        yield return new WaitForSeconds(2);
        panelList[0].text.GetComponent<Text>().text = "먼저 공을 위로 던져볼게요";
        panelList[0].panel.SetActive(false);
        panelList[3].panel.SetActive(true);
    }
    IEnumerator Turorial1Step2()
    {
        panelList[3].panel.SetActive(false);
        panelList[0].panel.SetActive(true);
        panelList[0].text.GetComponent<Text>().text = "잘하셨어요! 저희의 목표는 최대한 적은 공을 이용해서 모든 쿠키를 제거하는거예요";
        yield return new WaitForSeconds(3);
        panelList[0].text.GetComponent<Text>().text = "남은 공의 개수와 달성률은 상단에 표시가 돼요";
        yield return new WaitForSeconds(3);
        panelList[0].panel.SetActive(false);
        panelList[4].panel.SetActive(true);
        yield return new WaitForSeconds(2);
        panelList[4].panel.SetActive(false);
        panelList[0].panel.SetActive(true);
        panelList[0].text.GetComponent<Text>().text = "이번에는 각도를 줘서 던져볼게요";
        yield return new WaitForSeconds(2);
        panelList[0].panel.SetActive(false);
    }
}
