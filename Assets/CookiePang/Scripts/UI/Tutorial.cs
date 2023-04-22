using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
[Serializable]
public class DefaultTutorialStruct
{
    public string name;
    public GameObject panel;
    public Text text;
}
[Serializable]
public class BombStruct
{
    public string name;
    public GameObject panel;
    public Text text;
}
[Serializable]
public class PoisonStruct
{
    public string name;
    public GameObject panel;
    public Text text;
}
[Serializable]
public class JellyStruct
{
    public string name;
    public GameObject panel;
    public Text text;
}
[Serializable]
public class TeleportationStruct
{
    public string name;
    public GameObject panel;
    public Text text;
}
[Serializable]
public class PowerStruct
{
    public string name;
    public GameObject panel;
    public Text text;
}
[Serializable]
public class CandyStruct
{
    public string name;
    public GameObject panel;
    public Text text;
}
public class Tutorial : MonoBehaviour
{
    public List<DefaultTutorialStruct> state1TutorialList = new List<DefaultTutorialStruct>();
    public List<BombStruct> state3TutorialList = new List<BombStruct>();
    public List<PoisonStruct> state5TutorialList = new List<PoisonStruct>();
    public List<JellyStruct> state7TutorialList = new List<JellyStruct>();
    public List<TeleportationStruct> state11TutorialList = new List<TeleportationStruct>();
    public List<PowerStruct> state14TutorialList = new List<PowerStruct>();
    public List<CandyStruct> state17TutorialList = new List<CandyStruct>();
    void Start()
    {
        if(StageManager.instance.currentIndex == 1) 
        {
            StartCoroutine(Turorial1());
        }else if(StageManager.instance.currentIndex == 3)
        {
            StartCoroutine(Turorial3());
        }
        else if (StageManager.instance.currentIndex == 5)
        {
            StartCoroutine(Turorial5());
        }
        else if (StageManager.instance.currentIndex == 7)
        {
            StartCoroutine(Turorial7());
        }
        else if (StageManager.instance.currentIndex == 11)
        {
            StartCoroutine(Turorial11());
        }
        else if (StageManager.instance.currentIndex == 14)
        {
            StartCoroutine(Turorial14());
        }
        else if (StageManager.instance.currentIndex == 17)
        {
            StartCoroutine(Turorial17());
        }
    }
    IEnumerator Turorial1()
    {
        state1TutorialList[0].panel.SetActive(true);
        state1TutorialList[0].text.GetComponent<Text>().text = "간단하게 게임하는 법을 알려줄게요.";
        yield return new WaitForSeconds(3);
        state1TutorialList[0].text.GetComponent<Text>().text = "저희의 목표는 별사탕을 던져서 \n 모든 쿠키를 제거하는거예요";
        yield return new WaitForSeconds(3);
        state1TutorialList[0].panel.SetActive(false);
        state1TutorialList[1].panel.SetActive(true);
        yield return new WaitForSeconds(3);
        state1TutorialList[1].panel.SetActive(false);
        state1TutorialList[2].panel.SetActive(true);
        yield return new WaitForSeconds(3);
        state1TutorialList[2].panel.SetActive(false);
        state1TutorialList[0].panel.SetActive(true);
        state1TutorialList[0].text.GetComponent<Text>().text = "공은 아래의 슬라이더를 움직이거나 \n 화면을 직접 터치해서 던질 수 있어요";
        yield return new WaitForSeconds(2);
        state1TutorialList[0].panel.SetActive(false);
        state1TutorialList[3].panel.SetActive(true);
        yield return new WaitForSeconds(3);
        state1TutorialList[0].text.GetComponent<Text>().text = "남은 별사탕의 개수와 \n 달성률은 상단에 표시가 돼요";
        state1TutorialList[0].panel.SetActive(true);
        state1TutorialList[3].panel.SetActive(false);
        yield return new WaitForSeconds(2);
        state1TutorialList[0].text.GetComponent<Text>().text = "이제 플레이합시다! \n 행운을 빌게요~";
        yield return new WaitForSeconds(2);
        state1TutorialList[0].panel.SetActive(false);
    }
    IEnumerator Turorial3()
    {
        state3TutorialList[0].panel.SetActive(true);
        state3TutorialList[0].text.GetComponent<Text>().text = "폭탄 블럭은 주변 한 칸 범위 \n 블럭에게 1의 피해를 입혀요";
        yield return new WaitForSeconds(3);
        state3TutorialList[0].panel.SetActive(false);
        state3TutorialList[1].panel.SetActive(true);
        yield return new WaitForSeconds(3);
        state3TutorialList[1].panel.SetActive(false);
    }
    IEnumerator Turorial5()
    {
        state5TutorialList[0].panel.SetActive(true);
        state5TutorialList[0].text.GetComponent<Text>().text = "독극물을 부수면 \n 별사탕의 개수가 1개 줄어들어요. \n 최대한 부수지 않도록 해주세요!";
        yield return new WaitForSeconds(3);
        state5TutorialList[0].panel.SetActive(false);
        state5TutorialList[1].panel.SetActive(true);
        yield return new WaitForSeconds(3);
        state5TutorialList[1].panel.SetActive(false);
    }
    IEnumerator Turorial7()
    {
        state7TutorialList[0].panel.SetActive(true);
        state7TutorialList[0].text.GetComponent<Text>().text = "젤리 블럭은 별사탕으로 \n 부술 수 없어요";
        yield return new WaitForSeconds(3);
        state7TutorialList[0].text.GetComponent<Text>().text = "하지만 폭탄 블럭을 이용하면 \n 젤리 블럭을 제거할 수 있을거예요!";
        yield return new WaitForSeconds(3);
        state7TutorialList[0].panel.SetActive(false);
        state7TutorialList[1].panel.SetActive(true);
        yield return new WaitForSeconds(3);
        state7TutorialList[1].panel.SetActive(false);

    }
    IEnumerator Turorial11()
    {
        state11TutorialList[0].panel.SetActive(true);
        state11TutorialList[0].text.GetComponent<Text>().text = "포탈 블럭은 별사탕을 \n 반대편 포탈로 이동시켜줘요. ";
        yield return new WaitForSeconds(3);
        state11TutorialList[0].panel.SetActive(false);
        state11TutorialList[1].panel.SetActive(true);
        yield return new WaitForSeconds(3);
        state11TutorialList[1].panel.SetActive(false);
    }
    IEnumerator Turorial14()
    {
        state14TutorialList[0].panel.SetActive(true);
        state14TutorialList[0].text.GetComponent<Text>().text = "파워 아이템은 공의 \n 피해량을 1 증가시켜줘요. \n 공의 데미지는 하단에 표시돼요.";
        yield return new WaitForSeconds(3);
        state14TutorialList[0].panel.SetActive(false);
        state14TutorialList[1].panel.SetActive(true);
        yield return new WaitForSeconds(3);
        state14TutorialList[1].panel.SetActive(false);
        state14TutorialList[2].panel.SetActive(true);
        yield return new WaitForSeconds(3);
        state14TutorialList[2].panel.SetActive(false);
    }
    IEnumerator Turorial17()
    {
        state17TutorialList[0].panel.SetActive(true);
        state17TutorialList[0].text.GetComponent<Text>().text = "새로운 게임 모드인 \n 사탕 모으기 모드예요. \n 사탕을 모두 모으면 스테이지가 클리어돼요.";
        yield return new WaitForSeconds(3);
        state17TutorialList[0].panel.SetActive(false);
        state17TutorialList[1].panel.SetActive(true);
        yield return new WaitForSeconds(3);
        state17TutorialList[1].panel.SetActive(false);
        state17TutorialList[2].panel.SetActive(true);
        yield return new WaitForSeconds(3);
        state17TutorialList[2].panel.SetActive(false);
    }
}
