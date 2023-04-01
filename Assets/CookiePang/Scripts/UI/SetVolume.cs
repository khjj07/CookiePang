using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
public class SetVolume : MonoBehaviour
{
    public AudioMixer mixer;
    public Slider slider;
    void Start()
    {
        slider.value = SoundManager.instance.GetComponent<AudioSource>().volume;
        
    }

    // Update is called once per frame
    void Update()
    {
        SoundManager.instance.SetVolume(0, slider.value);
        SoundManager.instance.GetComponent<AudioSource>().volume = slider.value;
    }
}
