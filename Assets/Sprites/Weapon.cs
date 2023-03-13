using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Collidable
{
    protected EdgeCollider2D edgecollider;

    // Damage struct
    public int[] damagePoint = { 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13 };
    public float[] pushForce = { 4, 4.5f, 5, 5.5f, 6, 6.5f, 7, 7.5f, 8, 8.5f, 9, 9.5f };

    // Upgrade
    public int weaponLevel = 0;
    protected SpriteRenderer spriteRenderer;

    // Swing
    protected Animator anim;
    protected float cooldown = 0.5f;
    protected float lastSwing;

    protected override void Start()
    {
        edgecollider = GetComponent<EdgeCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    protected override void Update()
    {
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

    protected override void OnCollide(Collider2D coll)
    {
        if (coll.tag == "Fighter")
        {
            if (coll.name == "Player")
                return;

            // create damage object then send it to the fighter we are hit
            Damage dmg = new Damage
            {
                damageAmount = damagePoint[weaponLevel],
                origin = transform.position,
                pushForce = pushForce[weaponLevel]
            };

            coll.SendMessage("ReceiveDamage", dmg);
            Debug.Log(coll.name);
        }
    }
    protected virtual void Swing()
    {
    }

}

