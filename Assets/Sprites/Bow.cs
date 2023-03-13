using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : Weapon
{
    public int[] damagePointArrow = { 1, 2, 2, 4, 4, 6, 6, 8, 8, 10, 10 };
    public float[] pushForceArrow = { 1, 1.25f, 1.5f, 2, 2.5f, 3f, 3.5f, 4, 4.5f, 5, 5.5f };
    protected override void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        if (x != 0 || y != 0)
        {
            anim.SetFloat("X", x);
            anim.SetFloat("Y", y);
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
        else if(Input.GetMouseButtonDown(0)== false)
            anim.SetBool("Attack", false);

    }
    protected override void OnCollide(Collider2D coll)
    {
        if (coll.tag == "Arrow")
        {
            Damage dmg = new Damage
            {
                damageAmount = damagePointArrow[weaponLevel],
                origin = transform.position,
                pushForce = pushForceArrow[weaponLevel]
            };
            coll.SendMessage("getDamage", dmg);
            Debug.Log(coll.name);
        }
    }

    protected override void Swing()
    {
        anim.SetBool("Attack", true);
    }

    public void UpgradeSubWeapon()
    {
        weaponLevel++;
        spriteRenderer.sprite = GameManager.instance.subWeaponSprites[weaponLevel];
    }
}
