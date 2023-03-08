using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Stage : MonoBehaviour
{
    public StageAsset asset;

    public void Start()
    {
        asset.Initialize();
        GameManager.instance.isPlay = true;
    }    

    public void Update()
    {
       if(asset.IsClear())
        {

            //Å¬¸®¾î
        }
    }

    public int GetStars()
    {
        return asset.GetStars();
    }
}
