using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StageMode
{ 
    Default,
    Special
}
public class Stage : MonoBehaviour
{
    public StageAsset asset;

    public void Start()
    {
        asset.Initialize();
        GameManager.instance.isPlay = true;
    }    

    public bool IsClear()
    {
       return asset.IsClear();
    }

    public int GetStars()
    {
        return asset.GetStars();
    }
}
