using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Weapon
{
    protected override void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        if (x < 0)
           {
               anim.SetBool("Left", true);
               anim.SetBool("Down", false);
               anim.SetBool("Up", false);
               anim.SetBool("Right", false);
               spriteRenderer.sortingOrder = 3;
           }
           else if (x > 0)
           {
               anim.SetBool("Left", false);
               anim.SetBool("Down", false);
               anim.SetBool("Up", false);
               anim.SetBool("Right", true);
               spriteRenderer.sortingOrder = 1;
           }
           if (y < 0)
           {
               anim.SetBool("Left", false);
               anim.SetBool("Down", true);
               anim.SetBool("Up", false);
               anim.SetBool("Right", false);
               spriteRenderer.sortingOrder = 3;
           }
           else if (y > 0)
           {
               anim.SetBool("Left", false);
               anim.SetBool("Down", false);
               anim.SetBool("Up", true);
               anim.SetBool("Right", false);
               spriteRenderer.sortingOrder = 1;
           }
        edgecollider.OverlapCollider(filter, hits);
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i] == null)
                continue;
            OnCollide(hits[i]);
            hits[i] = null;
        }
        if (Input.GetMouseButton(0))
        {
            if (Time.time - lastSwing > cooldown)
            {
                lastSwing = Time.time;
                Swing();
            }
        }
    }

    protected override void Swing()
    {
        anim.SetTrigger("Swing");
    }

    public void UpgradeMainWeapon()
    {
        weaponLevel++;
        spriteRenderer.sprite = GameManager.instance.mainWeaponSprites[weaponLevel];
    }
}
