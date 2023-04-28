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
        int ran = Random.Range(1, 4);
        switch (ran)
        {
            case 1:
                SoundManager.instance.PlaySound(0, "IngameSound1");
                break;
            case 2:
                SoundManager.instance.PlaySound(0, "IngameSound2");
                break;
            case 3:
                SoundManager.instance.PlaySound(0, "IngameSound3");
                break;
        }
        
    }    

    public void Update()
    {
       if(asset.IsClear())
        {
            GameManager.instance.isClear = true;
        }
       if (asset.IsOver())
       {
           GameManager.instance.isOver = true;
       }
        GameManager.instance.goal = asset.GetGoal();
    }

    public int GetStars()
    {
        return asset.GetStars();
    }
}
