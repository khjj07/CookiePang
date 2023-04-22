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
        state1TutorialList[0].text.GetComponent<Text>().text = "�����ϰ� �����ϴ� ���� �˷��ٰԿ�.";
        yield return new WaitForSeconds(3);
        state1TutorialList[0].text.GetComponent<Text>().text = "������ ��ǥ�� �������� ������ \n ��� ��Ű�� �����ϴ°ſ���";
        yield return new WaitForSeconds(3);
        state1TutorialList[0].panel.SetActive(false);
        state1TutorialList[1].panel.SetActive(true);
        yield return new WaitForSeconds(3);
        state1TutorialList[1].panel.SetActive(false);
        state1TutorialList[2].panel.SetActive(true);
        yield return new WaitForSeconds(3);
        state1TutorialList[2].panel.SetActive(false);
        state1TutorialList[0].panel.SetActive(true);
        state1TutorialList[0].text.GetComponent<Text>().text = "���� �Ʒ��� �����̴��� �����̰ų� \n ȭ���� ���� ��ġ�ؼ� ���� �� �־��";
        yield return new WaitForSeconds(2);
        state1TutorialList[0].panel.SetActive(false);
        state1TutorialList[3].panel.SetActive(true);
        yield return new WaitForSeconds(3);
        state1TutorialList[0].text.GetComponent<Text>().text = "���� �������� ������ \n �޼����� ��ܿ� ǥ�ð� �ſ�";
        state1TutorialList[0].panel.SetActive(true);
        state1TutorialList[3].panel.SetActive(false);
        yield return new WaitForSeconds(2);
        state1TutorialList[0].text.GetComponent<Text>().text = "���� �÷����սô�! \n ����� ���Կ�~";
        yield return new WaitForSeconds(2);
        state1TutorialList[0].panel.SetActive(false);
    }
    IEnumerator Turorial3()
    {
        state3TutorialList[0].panel.SetActive(true);
        state3TutorialList[0].text.GetComponent<Text>().text = "��ź ���� �ֺ� �� ĭ ���� \n ������ 1�� ���ظ� ������";
        yield return new WaitForSeconds(3);
        state3TutorialList[0].panel.SetActive(false);
        state3TutorialList[1].panel.SetActive(true);
        yield return new WaitForSeconds(3);
        state3TutorialList[1].panel.SetActive(false);
    }
    IEnumerator Turorial5()
    {
        state5TutorialList[0].panel.SetActive(true);
        state5TutorialList[0].text.GetComponent<Text>().text = "���ع��� �μ��� \n �������� ������ 1�� �پ����. \n �ִ��� �μ��� �ʵ��� ���ּ���!";
        yield return new WaitForSeconds(3);
        state5TutorialList[0].panel.SetActive(false);
        state5TutorialList[1].panel.SetActive(true);
        yield return new WaitForSeconds(3);
        state5TutorialList[1].panel.SetActive(false);
    }
    IEnumerator Turorial7()
    {
        state7TutorialList[0].panel.SetActive(true);
        state7TutorialList[0].text.GetComponent<Text>().text = "���� ���� ���������� \n �μ� �� �����";
        yield return new WaitForSeconds(3);
        state7TutorialList[0].text.GetComponent<Text>().text = "������ ��ź ���� �̿��ϸ� \n ���� ���� ������ �� �����ſ���!";
        yield return new WaitForSeconds(3);
        state7TutorialList[0].panel.SetActive(false);
        state7TutorialList[1].panel.SetActive(true);
        yield return new WaitForSeconds(3);
        state7TutorialList[1].panel.SetActive(false);

    }
    IEnumerator Turorial11()
    {
        state11TutorialList[0].panel.SetActive(true);
        state11TutorialList[0].text.GetComponent<Text>().text = "��Ż ���� �������� \n �ݴ��� ��Ż�� �̵��������. ";
        yield return new WaitForSeconds(3);
        state11TutorialList[0].panel.SetActive(false);
        state11TutorialList[1].panel.SetActive(true);
        yield return new WaitForSeconds(3);
        state11TutorialList[1].panel.SetActive(false);
    }
    IEnumerator Turorial14()
    {
        state14TutorialList[0].panel.SetActive(true);
        state14TutorialList[0].text.GetComponent<Text>().text = "�Ŀ� �������� ���� \n ���ط��� 1 �����������. \n ���� �������� �ϴܿ� ǥ�õſ�.";
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
        state17TutorialList[0].text.GetComponent<Text>().text = "���ο� ���� ����� \n ���� ������ ��忹��. \n ������ ��� ������ ���������� Ŭ����ſ�.";
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
