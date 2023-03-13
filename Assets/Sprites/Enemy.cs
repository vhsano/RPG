using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Mover
{
    // Exp
    public int xpValue = 1;

    // Logic
    public float triggerLenght = 1;
    public float chaseLenght = 5;
    protected bool chasing;
    protected bool collidingWithPlayer;
    protected Transform playerTransform;
    protected Vector3 startingPosition;

    // Hitbox
    public ContactFilter2D filter;
    protected BoxCollider2D hitbox;
    protected Collider2D[] hits = new Collider2D[10];

    protected override void Start()
    {
        base.Start();
        playerTransform = GameManager.instance.player.transform;
        //playerTransform = GameObject.Find("Player").transform;
        startingPosition = transform.position;
        hitbox = transform.GetChild(0).GetComponent<BoxCollider2D>();
    }

    protected override void UpdateMotor(Vector3 input)
    {
        if (input.x > 0)
            transform.localScale = new Vector3(1, 1, 1);
        else
        {
            if (input.x < 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
        }
        base.UpdateMotor(input);
    }

    protected virtual void FixedUpdate()
    {
        // is the player in range


        collidingWithPlayer = false;
        if (Vector3.Distance(playerTransform.position,startingPosition) < chaseLenght)
        {
            if (Vector3.Distance(playerTransform.position, startingPosition) < triggerLenght)
            {
                chasing = true;
                anim.SetBool("IsMoving", true);
            }

            if (chasing)
            {
                if (!collidingWithPlayer)
                {
                    UpdateMotor((playerTransform.position - transform.position).normalized);
                }
            }
            else
            {
                UpdateMotor(startingPosition - transform.position);
            }
        }
        else
        {
            UpdateMotor(startingPosition - transform.position);
            chasing = false;
            anim.SetBool("IsMoving", false);
        }

        
        boxCollider.OverlapCollider(filter, hits);
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i] == null)
                continue;

            if(hits[i].tag == "Fighter" && hits[i].name == "Player")
            {
                collidingWithPlayer = true;
            }
            hits[i] = null;
        }
    }

    protected override void ReceiveDamage(Damage dmg)
    {
        if (Time.time - lastImmune > immuneTime)
        {
            lastImmune = Time.time;
            hitPoint -= dmg.damageAmount;
            pushDir = (transform.position - dmg.origin).normalized * dmg.pushForce;

            anim.SetTrigger("BeAttack");
            GameManager.instance.ShowText(dmg.damageAmount.ToString(), 25, Color.red, transform.position + new Vector3(0, 0.16f, 0), Vector3.up * 25, 0.5f);
            if (hitPoint <= 0)
            {
                hitPoint = 0;
                Death();
            }
        }
        
        
    }
    protected override void Death()
    {
        Destroy(gameObject);
        GameManager.instance.GrantXp(xpValue);
        GameManager.instance.ShowText("+" + xpValue, 25, Color.magenta, playerTransform.position, Vector3.up * 40, 1.0f);
    }
}
