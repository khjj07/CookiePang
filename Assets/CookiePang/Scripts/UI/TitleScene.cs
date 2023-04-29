using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TitleScene : MonoBehaviour
{
    [SerializeField] private Slider[] slider;
    public Sprite[] sprites;
    public GameObject onOffButton;
    //private bool DontDestroy = true; 
    private void Awake()
    {

        //if (DontDestroy)
        //    DontDestroyOnLoad(this.gameObject);
    }
    private void Start()
    {
        //SetResolution();
        slider[0].value = SoundManager.instance.Player[0].Volume;
        slider[1].value = SoundManager.instance.Player[1].Volume;
        if (SoundManager.instance.isStartSound) 
        { 
            SoundManager.instance.PlaySound(0, "MainSound"); 
            SoundManager.instance.isStartSound = false; 
        }
        if (EffectManager.instance.isParticle == false)
            onOffButton.GetComponent<Image>().sprite = sprites[1];
        else
            onOffButton.GetComponent<Image>().sprite = sprites[0];
        
    }

    public void SetResolution()
    {
        int setWidth = 1080; // ����� ���� �ʺ�
        int setHeight = 1920; // ����� ���� ����

        int deviceWidth = Screen.width; // ��� �ʺ� ����
        int deviceHeight = Screen.height; // ��� ���� ����

        Screen.SetResolution(setWidth, (int)(((float)deviceHeight / deviceWidth) * setWidth), true); // SetResolution �Լ� ����� ����ϱ�
        if ((float)setWidth / setHeight < (float)deviceWidth / deviceHeight) // ����� �ػ� �� �� ū ���
        {
            float newWidth = ((float)setWidth / setHeight) / ((float)deviceWidth / deviceHeight); // ���ο� �ʺ�
            Camera.main.rect = new Rect((1f - newWidth) / 2f, 0f, newWidth, 1f); // ���ο� Rect ����
        }
        else // ������ �ػ� �� �� ū ���
        {
            float newHeight = ((float)deviceWidth / deviceHeight) / ((float)setWidth / setHeight); // ���ο� ����
            Camera.main.rect = new Rect(0f, (1f - newHeight) / 2f, 1f, newHeight); // ���ο� Rect ����
        }
    }

}
