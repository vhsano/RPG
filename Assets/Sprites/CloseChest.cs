using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseChest : MonoBehaviour
{
    public Animator anim;
    public Transform lookAt;
    public bool Take;
    public bool Open;

    private void Start()
    {
        lookAt = GameObject.Find("Player").transform;
    }
    protected void Update()
    {
        // Close chest if u move away
        float deltaX = lookAt.position.x - transform.position.x;
        if (deltaX > 0.3 || deltaX < -0.3)
        {
            Open = false;
            anim.SetBool("Open", Open);
        }

        float deltaY = lookAt.position.y - transform.position.y;
        if (deltaY > 0.3 || deltaY < -0.3)
        {
            Open = false;
            anim.SetBool("Open", Open);
        }
    }
}
