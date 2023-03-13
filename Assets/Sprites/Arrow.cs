using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class Arrow : Weapon
{
    public int damagePointArrow;
    public float pushForceArrow; 

    private Vector3 shootDir;

    private void Awake()
    {
        weaponLevel = 0;
    }

    public void Setup(Vector3 shootDir)
    {
        this.shootDir = shootDir;
        transform.eulerAngles = new Vector3(0, 0, UtilsClass.GetAngleFromVectorFloat(shootDir));
        Destroy(gameObject, 0.3f);
    }

    public void getDamage(Damage damage)
    {
        damagePointArrow = damage.damageAmount;
        pushForceArrow = damage.pushForce;
    }

    protected override void Update()
    {
        float moveSpeed = 12f;
        transform.position += shootDir * moveSpeed * Time.deltaTime;
        edgecollider.OverlapCollider(filter, hits);
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i] == null)
                continue;
            OnCollide(hits[i]);
            hits[i] = null;
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
                damageAmount = damagePointArrow,
                origin = transform.position,
                pushForce = pushForceArrow
            };

            coll.SendMessage("ReceiveDamage", dmg);
            Destroy(this.gameObject);
        }
    }

    public void ArrowUpgrade()
    {
        //weaponLevel++;
    }
}
