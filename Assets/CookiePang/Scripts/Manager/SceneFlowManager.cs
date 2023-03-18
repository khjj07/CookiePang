using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneFlowManager : MonoBehaviour
{
    public GameObject settingPanel;
    // Start is called before the first frame update
    public static void ChangeScene(string name)
    {
        SceneManager.LoadSceneAsync(name,LoadSceneMode.Single);
    }

    public void GameQuitButton() 
    {
        Application.Quit();
    }
    public void OnSettingPanel()
    {
        if (settingPanel.activeSelf)
        {
            settingPanel.SetActive(false);
        }
        else
        {
            settingPanel.SetActive(true);
        }
    }
}
