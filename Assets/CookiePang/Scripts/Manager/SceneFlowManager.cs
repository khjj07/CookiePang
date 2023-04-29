using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneFlowManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static void ChangeScene(string name)
    {
        SceneManager.LoadSceneAsync(name,LoadSceneMode.Single);
    }
    public static void ChangeSceneNotAsync(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void QuitGame() 
    {
        Application.Quit();
    }
}
