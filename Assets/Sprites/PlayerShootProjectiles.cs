using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey;
using CodeMonkey.Utils;

public class PlayerShootProjectiles : MonoBehaviour
{
    [SerializeField] private Transform pfArrow;
    public GameObject Bow;

    private void Awake()
    {
            GetComponent<PlayerAimBow>().OnShoot += PlayerShootProjectiles_OnShoot;
    }

    private void PlayerShootProjectiles_OnShoot(object sender,PlayerAimBow.OnShootEvenArgs e)
    {
        if (Bow.activeSelf == true)
        {
            Transform arrowTransform = Instantiate(pfArrow, e.bowEndPointPosition, Quaternion.identity);

            Vector3 shootDir = (e.shootPosition - e.bowEndPointPosition).normalized;
            arrowTransform.GetComponent<Arrow>().Setup(shootDir);
        }
    }
}
