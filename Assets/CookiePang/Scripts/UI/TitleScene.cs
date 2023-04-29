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
        int setWidth = 1080; // 사용자 설정 너비
        int setHeight = 1920; // 사용자 설정 높이

        int deviceWidth = Screen.width; // 기기 너비 저장
        int deviceHeight = Screen.height; // 기기 높이 저장

        Screen.SetResolution(setWidth, (int)(((float)deviceHeight / deviceWidth) * setWidth), true); // SetResolution 함수 제대로 사용하기
        if ((float)setWidth / setHeight < (float)deviceWidth / deviceHeight) // 기기의 해상도 비가 더 큰 경우
        {
            float newWidth = ((float)setWidth / setHeight) / ((float)deviceWidth / deviceHeight); // 새로운 너비
            Camera.main.rect = new Rect((1f - newWidth) / 2f, 0f, newWidth, 1f); // 새로운 Rect 적용
        }
        else // 게임의 해상도 비가 더 큰 경우
        {
            float newHeight = ((float)deviceWidth / deviceHeight) / ((float)setWidth / setHeight); // 새로운 높이
            Camera.main.rect = new Rect(0f, (1f - newHeight) / 2f, 1f, newHeight); // 새로운 Rect 적용
        }
    }

}
