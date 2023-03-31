using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Stage : MonoBehaviour
{
    public StageAsset asset;

    public void Start()
    {
        asset = StageManager.instance.GetCurrentStage();
        if (asset)
            asset.Initialize();
        else
            Destroy(gameObject);
        
        GameManager.instance.PlayGame();
        SoundManager.instance.PlaySound(0, "IngameSound");
    }    

    public void Update()
    {
       if(asset.IsClear())
        {
            GameManager.instance.isClear = true;
        }
    }

    public int GetStars()
    {
        return asset.GetStars();
    }
}
