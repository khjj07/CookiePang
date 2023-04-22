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
        panelList[0].text.GetComponent<Text>().text = "�����ϰ� �����ϴ� ���� �˷��ٰԿ�.";
        yield return new WaitForSeconds(2);
        panelList[0].text.GetComponent<Text>().text = "������ ��ǥ�� �������� ������ \n ��� ��Ű�� �����ϴ°ſ���";
        yield return new WaitForSeconds(4);
        panelList[0].panel.SetActive(false);
        panelList[1].panel.SetActive(true);
        yield return new WaitForSeconds(3);
        panelList[1].panel.SetActive(false);
        panelList[2].panel.SetActive(true);
        yield return new WaitForSeconds(3);
        panelList[2].panel.SetActive(false);
        panelList[0].panel.SetActive(true);
        panelList[0].text.GetComponent<Text>().text = "���� �Ʒ��� �����̴��� �����̰ų� \n ȭ���� ���� ��ġ�ؼ� ���� �� �־��";
        yield return new WaitForSeconds(2);
        panelList[0].text.GetComponent<Text>().text = "���� ���� ���� �������Կ�";
        panelList[0].panel.SetActive(false);
        panelList[3].panel.SetActive(true);
    }
    IEnumerator Turorial1Step2()
    {
        panelList[3].panel.SetActive(false);
        panelList[0].panel.SetActive(true);
        panelList[0].text.GetComponent<Text>().text = "���ϼ̾��! ������ ��ǥ�� �ִ��� ���� ���� �̿��ؼ� ��� ��Ű�� �����ϴ°ſ���";
        yield return new WaitForSeconds(3);
        panelList[0].text.GetComponent<Text>().text = "���� ���� ������ �޼����� ��ܿ� ǥ�ð� �ſ�";
        yield return new WaitForSeconds(3);
        panelList[0].panel.SetActive(false);
        panelList[4].panel.SetActive(true);
        yield return new WaitForSeconds(2);
        panelList[4].panel.SetActive(false);
        panelList[0].panel.SetActive(true);
        panelList[0].text.GetComponent<Text>().text = "�̹����� ������ �༭ �������Կ�";
        yield return new WaitForSeconds(2);
        panelList[0].panel.SetActive(false);
    }
}
