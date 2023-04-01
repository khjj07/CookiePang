using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleSceneSound : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SoundManager.instance.PlaySound(0, "MainSound");
    }

}
