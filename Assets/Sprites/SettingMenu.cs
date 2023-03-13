using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingMenu : MonoBehaviour
{
    public Animator anim;
    public bool showSettingMenu = false;
    public void Exit()
    {
        UnityEngine.Application.Quit();
    }

    public void Continue()
    {
        anim.SetTrigger("Hide");
        showSettingMenu = false;
    }
    private void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            if (showSettingMenu == false)
            {
                anim.SetTrigger("Show");
                showSettingMenu = true;
            }
            else if (showSettingMenu == true)
            {
                anim.SetTrigger("Hide");
                showSettingMenu = false;
            }
        }
    }
}
