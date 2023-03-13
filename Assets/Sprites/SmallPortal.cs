using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallPortal : Collidable
{
    public GameObject Player;
    public float x;
    public float y;

    protected override void Start()
    {
        base.Start();
        Player = GameObject.Find("Player");
    }
    protected override void OnCollide(Collider2D coll)
    {
        if (coll.name == "Player")
        {
            Player.transform.position = new Vector3(x, y, 0);
        }
    }
}
