using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Mover : Fighter
{
    protected BoxCollider2D boxCollider;
    public Animator anim;
    protected RaycastHit2D hit;
    protected Vector3 moveDelta;
    protected bool IsMoving;
    public float speed = 1.5f;
    protected float x, y;

    
    protected virtual void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }
    protected virtual void UpdateMotor(Vector3 input)
    {
        moveDelta = new Vector3(input.x * speed, input.y * speed, 0);
        /*if (moveDelta.x > 0)
            transform.localScale = new Vector3(1, 1, 1);
        else
        {
            if (moveDelta.x < 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else
            {
                if (moveDelta.y > 0)
                    transform.localScale = new Vector3(-1, 1, 1);
                else
                {
                    if (moveDelta.y < 0)
                    {
                        transform.localScale = new Vector3(1, 1, 1);
                    }
                }
            }
        }*/
        

        //add push vector, if any
        moveDelta += pushDir;

        // reduce push force every frame, based off recovery speed
        pushDir = Vector3.Lerp(pushDir, Vector3.zero, pushRecoverySpeed); 

        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(0, moveDelta.y), Mathf.Abs(moveDelta.y * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));
        if (hit.collider == null)
        {
            transform.Translate(0, moveDelta.y * Time.deltaTime, 0);
        }

        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(moveDelta.x, 0), Mathf.Abs(moveDelta.x * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));
        if (hit.collider == null)
        {
            transform.Translate(moveDelta.x  * Time.deltaTime, 0, 0);
        }
    }
}
