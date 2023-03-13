using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemy : Fighter
{
    public int xpValue = 1;

    public float Range;

    public Transform Target;

    bool Detected = false;

    Vector2 Direction;

    public GameObject Fireball;

    public float FireRate;

    public Transform shootPoint;

    float nextTimeToFire = 1.5f;

    public float Force;
    // use this for  initalization

    void Start()
    {
        Target = GameObject.Find("Player").transform;
    }

    private void FixedUpdate()
    {
        Vector3 LookAt = (Target.position - transform.position).normalized;
        if (LookAt.x > 0)
            transform.localScale = new Vector3(1, 1, 1);
        else
        {
            if (LookAt.x < 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
        }
    }

    private void Update()
    {
        Vector2 targetPos = Target.position;
        Direction = targetPos - (Vector2)transform.position;
        RaycastHit2D rayInfo = Physics2D.Raycast(transform.position, Direction, Range);

        if(rayInfo)
        {
            if(rayInfo.collider.gameObject.tag == "Fighter" && rayInfo.collider.gameObject.name == "Player")
            {
                if(Detected == false)
                {
                    Detected = true;
                }
            } 
            else
            {
                if(Detected == true)
                {
                    Detected = false;
                }
            }
        }

        if(Detected)
        {
            if(Time.time > nextTimeToFire)
            {
                nextTimeToFire = Time.time + 1 / FireRate;
                shoot();
            }
        }
    }

    void shoot()
    {
        GameObject FireballIns = Instantiate(Fireball, shootPoint.position, Quaternion.identity);
        FireballIns.GetComponent<Rigidbody2D>().AddForce(Direction * Force);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, Range);
    }
    protected override void Death()
    {
        Destroy(gameObject);
        GameManager.instance.GrantXp(xpValue);
        GameManager.instance.ShowText("+" + xpValue, 25, Color.magenta, Target.position, Vector3.up * 40, 1.0f);
    }
}
