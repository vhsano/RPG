using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Collectable
{
    public Animator anim;
    public int coin = 5;
    protected override void OnCollect()
    {
        Open = true;
        anim.SetBool("Open", Open);
        anim.SetBool("Take", Take);
        if (!Collect)
        {
            Collect = true;
            Take = true;
            GameManager.instance.coin += coin;
            GameManager.instance.ShowText("+" + coin + " coin!", 25, Color.yellow, transform.position, Vector3.up * 50, 1.0f); 
        }

    }
}
