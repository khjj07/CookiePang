using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
public class SetVolume : MonoBehaviour
{
    [SerializeField] private Slider slider;
    public void BgmSlider()
    {
        SoundManager.instance.SetVolume(0, slider.value);
        SoundManager.instance.GetComponent<AudioSource>().volume = slider.value;
    }
    public void SfxSlider()
    {
        SoundManager.instance.SetVolume(1, slider.value);
    }
    private void Update()
    {
    }
}
