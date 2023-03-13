using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    public float attackRange = 1f;
    protected override void FixedUpdate()
    {
        collidingWithPlayer = false;
        if (Vector3.Distance(playerTransform.position, startingPosition) < chaseLenght)
        {
            if (Vector3.Distance(playerTransform.position, startingPosition) < triggerLenght)
            {
                anim.SetBool("IsMoving", true);
                chasing = true;
            }
            if (chasing)
            {

                
                if (Vector3.Distance(transform.position, playerTransform.position) > attackRange)
                {
                    UpdateMotor((playerTransform.position - transform.position).normalized);
                }
                else
                {
                    anim.SetTrigger("Attack");
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

            if (hits[i].tag == "Fighter" && hits[i].name == "Player")
            {
                collidingWithPlayer = true;
            }
            hits[i] = null;
        }
    }
}
