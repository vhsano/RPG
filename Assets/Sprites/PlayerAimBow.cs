using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using System;

public class PlayerAimBow : MonoBehaviour
{
    public event EventHandler<OnShootEvenArgs> OnShoot;
    public class OnShootEvenArgs : EventArgs
    {
        public Vector3 shootPosition;
        public Vector3 bowEndPointPosition;
    }

    private Transform aimTransform;
    private Transform aimBowEndPointPosition;

    private void Awake()
    {
        aimTransform = transform.Find("Aim");
        aimBowEndPointPosition = aimTransform.Find("bowEndPointPosition");
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) == true)
        {
            HandleAiming();
        }
        else 
        {
            aimTransform.eulerAngles = new Vector3(0, 0, -90);
            aimTransform.position = new Vector3(GameManager.instance.player.transform.position.x,GameManager.instance.player.transform.position.y + 0.075f, 0);
        }
        if (Input.GetMouseButtonUp(0) == true)
        {
            Vector3 mousePosotion = UtilsClass.GetMouseWorldPosition();
            OnShoot?.Invoke(this, new OnShootEvenArgs
            {
                bowEndPointPosition = aimBowEndPointPosition.position,
                shootPosition = mousePosotion,
            });
        }
    }

    private void HandleAiming()
    {
        Vector3 mousePosotion = UtilsClass.GetMouseWorldPosition();
        Vector3 aimDirection = (mousePosotion - transform.position).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        aimTransform.eulerAngles = new Vector3(0, 0, angle);
    }
}
