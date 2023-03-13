using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : Collidable
{
    protected bool Collect;
    protected bool Take;
    protected bool Open;

    protected override void OnCollide(Collider2D coll)
    {
        if (coll.name == "Player")
            OnCollect();
    }
    protected virtual void OnCollect()
    {
        Open = true;
    }
}
