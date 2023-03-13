using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenu : MonoBehaviour
{
    public void PlayNew()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main");
    }

    public void Exit()
    {
        UnityEngine.Application.Quit();
    }
}
