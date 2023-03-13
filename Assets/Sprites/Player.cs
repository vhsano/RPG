using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Mover
{
    private bool isAlive = true;

    protected override void Death()
    {
        isAlive = false;
        GameManager.instance.deathMenu.SetTrigger("Show");
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void ReceiveDamage(Damage dmg)
    {
        if (!isAlive)
            return; 

        base.ReceiveDamage(dmg);
        GameManager.instance.OnHItponitChange();
    }
    protected override void UpdateMotor(Vector3 input)
    {

        if (input.x != 0 || input.y != 0)
        {
            anim.SetFloat("X", input.x);
            anim.SetFloat("Y", input.y);
            if (!IsMoving)
            {
                IsMoving = true;
                anim.SetBool("IsMoving", IsMoving);
            }
        }
        else
        {
            if (IsMoving)
            {
                IsMoving = false;
                anim.SetBool("IsMoving", IsMoving);
            }
        }
        base.UpdateMotor(input);
    }
    private void FixedUpdate()
    {
        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");
        if(isAlive)
            UpdateMotor(new Vector3(x, y, 0));
    }

    public void OnLevelUp()
    {
        maxHitPoint++;
        hitPoint = maxHitPoint;
        GameManager.instance.OnHItponitChange();
    }

    public void SetLevel(int level)
    {
        for (int i = 0; i < level; i++)
            OnLevelUp();
    }

    public void Heal(int healingAmount)
    {
        if (hitPoint == maxHitPoint)
            return;

        hitPoint += healingAmount;
        if (hitPoint > maxHitPoint)
            hitPoint = maxHitPoint;
        GameManager.instance.ShowText("+" + healingAmount.ToString() + "Hp", 25, Color.green, transform.position, Vector3.up * 30, 1.0f);
        GameManager.instance.OnHItponitChange();

    }

    public void Respawn()
    {
        Heal(maxHitPoint);
        isAlive = true;
        lastImmune = Time.time;
        pushDir = Vector3.zero;
    }
   
}
