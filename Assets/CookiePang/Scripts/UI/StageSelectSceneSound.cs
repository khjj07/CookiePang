using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StageSelectSceneSound : MonoBehaviour
{
    [SerializeField] private Slider[] slider;
    private void Start()
    {
        slider[0].value = SoundManager.instance.Player[0].Volume;
        slider[1].value = SoundManager.instance.Player[1].Volume;
    }
}
