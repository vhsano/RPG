using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : Collidable
{
    public Animator anim;
    protected override void OnCollide(Collider2D coll)
    {
        if(coll.name == "Player")
        {
            if( Input.GetKeyDown("space") && anim.GetBool("Hide") == true)
            {
                anim.SetBool("Show",true);
                anim.SetBool("Hide",false);
            }
            else if(Input.GetKeyDown("space") && anim.GetBool("Show") == true)
            {
                anim.SetBool("Show", false);
                anim.SetBool("Hide", true);
            }
        }
    }
}
