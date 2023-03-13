using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : Collidable
{
    public string[] Scenename;

    protected override void OnCollide(Collider2D coll)
    {
        if ( coll.name == "Player")
        {
            GameManager.instance.SaveState();
            string scenename = Scenename[Random.Range(0, Scenename.Length)];
            SceneManager.LoadScene(scenename);
        }
    }
}
