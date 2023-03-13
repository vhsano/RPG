using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapWeapon : MonoBehaviour
{
    public GameObject Sword;
    public GameObject Bow;
    private void Update()
    {
        if (Input.GetKeyDown("q"))
        {
            if (Bow.activeSelf == false)
            {
                Bow.SetActive(true);
                Sword.SetActive(false);
            }
            else
            {
                Bow.SetActive(false);
                Sword.SetActive(true);
            }
        }
    }
}
