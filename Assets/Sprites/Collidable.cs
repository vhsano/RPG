using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collidable : MonoBehaviour
{
    public ContactFilter2D filter;
    private BoxCollider2D boxcollider;
    protected Collider2D[] hits = new Collider2D[10];
    
    protected virtual void Start()
    {
        boxcollider = GetComponent<BoxCollider2D>();
    }

    protected virtual void Update()
    {
        // Collision work
        boxcollider.OverlapCollider(filter, hits);
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i] == null)
                continue;
            OnCollide(hits[i]);
            hits[i] = null;
        }
    }

    protected virtual void OnCollide(Collider2D coll)
    {
        if (coll.name == "Player")
            Debug.Log(coll.name);
    }
}
