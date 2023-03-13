using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrieBall : MonoBehaviour
{
    public int damagePointArrow;
    public float pushForceArrow;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Fighter")
        {
            if (collision.collider.name == "Player")
            {
                Damage dmg = new Damage
                {
                    damageAmount = damagePointArrow,
                    origin = transform.position,
                    pushForce = pushForceArrow
                };
                collision.collider.SendMessage("ReceiveDamage", dmg);
            }
            Destroy(this.gameObject);
        }
    }

    private void Update()
    {
        Destroy(gameObject, 1f);
    }
}
