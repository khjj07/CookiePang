using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
//이 게임에서는 거리에 비례해 사운드의 크기를 조절할 필요가 없기에 하나의 AudioSource로 AudioClip들을 돌려가며 실행시킬 것이다.
//배경음악을 실행할 AudioSource와 효과음을 실행할 AudioSource를 SoundManager의 자식 오브젝트로 설정


[Serializable]
public class PlayerStruct
{
    public AudioSource Instance;
    public float Volume =1f;
    public bool Loop = false;
    public List<SoundStruct> ClipList = new List<SoundStruct>();
    /*
    public PlayerStruct(AudioSource instance)
    {
        Instance = instance;
        Volume = 1f;
    }*/
    public PlayerStruct(AudioSource instance, float volume, bool loop)
    {
        Instance = instance;
        Volume = volume;
        Loop = loop;
    }
    public void PlaySound(string name, float volume = 1f)
    {
        Instance.volume = Volume * volume;
        Instance.clip = ClipList.Find(x => x.Name.Equals(name)).Clip;
        Instance.loop = Loop;
        if (Loop)
        {
            Instance.Play();
        }
        else
        {
            Instance.PlayOneShot(ClipList.Find(x => x.Name.Equals(name)).Clip, Volume * volume);
        }
            
    }

    public void SetVolume(float volume)
    {
        Volume = volume;
    }
}

[Serializable]
public class SoundStruct
{
    public string Name;
    public AudioClip Clip;
    public SoundStruct(string name, AudioClip clip)
    {
        Name = name;
        Clip = clip;
    }
}


public class SoundManager : Singleton<SoundManager>
{
    public bool isStartSound = true; //첫 시작 사운드
    private bool DontDestroy = true;
    [SerializeField]
    public List<PlayerStruct> Player;
    

    private void Awake()
    {
        if (DontDestroy)
            DontDestroyOnLoad(this.gameObject);
        
    }

    public void PlaySound(int channel, string name, float volume = 1f)
    {
        PlayerStruct player = Player[channel];
        player.PlaySound(name,volume);
    }
    public void SetVolume(int channel,float volume)
    {
        PlayerStruct player = Player[channel];
        player.SetVolume(volume);
    }
}