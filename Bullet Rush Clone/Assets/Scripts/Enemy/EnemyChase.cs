using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChase : MonoBehaviour
{
    [SerializeField] private Transform playerPos;
    [SerializeField] private float enemySpeed;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        transform.LookAt(new Vector3(playerPos.position.x,transform.position.y,playerPos.position.z));
        rb.velocity = transform.forward * (Time.fixedDeltaTime * enemySpeed);
    }
}
