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
        panel.SetActive(true);
        SoundManager.instance.PlaySound(1, "ButtonSound");
    }
    public void ExitPanel()
    {
        panel.SetActive(false);
        SoundManager.instance.PlaySound(1, "ButtonSound");
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

    private void Update()
    {
        if (GameManager.instance == null)
        {
            Time.timeScale = 1;
        }
        else
        {
            if (panel.active)
            {
                GameManager.instance.isPlay = false;
                Time.timeScale = 0;
            }
            else
            {
                GameManager.instance.isPlay = true;
                Time.timeScale = 1;
            }

            if(GameManager.instance.successPanel.activeSelf || GameManager.instance.failPanel.activeSelf)
            {
                GameManager.instance.isPlay = false;
            }
            else
            {
                GameManager.instance.isPlay = true;
            }
        }
    }
}
