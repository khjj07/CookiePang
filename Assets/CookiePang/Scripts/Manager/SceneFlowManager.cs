using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneFlowManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static void ChangeScene(string name)
    {
        //SoundManager.instance.PlaySound(1, "UiClickSound");
        SceneManager.LoadSceneAsync(name,LoadSceneMode.Single);
    }

    public void QuitGame() 
    {
        Application.Quit();
    }
}
