using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneFlowManager : MonoBehaviour
{

    [SerializeField] private GameObject panel;
    [SerializeField] private GameObject[] checkPanel;
    // Start is called before the first frame update
    public static void ChangeScene(string name)
    {
        SceneManager.LoadSceneAsync(name,LoadSceneMode.Single);
        GameManager.instance.isClear = false;
    }
    public void GameQuitButton() 
    {
        Application.Quit();
    }
    public void OpenPanel()
    {
        if (panel.activeSelf)
        {
            panel.SetActive(false);
        }
        else
        {
            panel.SetActive(true);
            SoundManager.instance.PlaySound(1, "ButtonSound");
        }
    }

    //다시체크
    public void ReCheckPanel(int index) 
    {
        if (checkPanel[index].activeSelf)
        {
            checkPanel[index].SetActive(false);
        }
        else
        {
            checkPanel[index].SetActive(true);
            SoundManager.instance.PlaySound(1, "ButtonSound");
        }
        
    }
   


}
