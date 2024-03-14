using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShootingManager : MonoBehaviour
{
    public Transform leftHand;
    public Transform rightHand;
    public float shootingRange = 10f;
    public float fireRate = 0.5f;
    private float nextFireTime = 0f;

    void Update()
    {
        if (Time.time > nextFireTime)
        {
            Collider[] enemies = Physics.OverlapSphere(transform.position, shootingRange, LayerMask.GetMask("Enemy"));

            if (enemies.Length >= 2)
            {
                Collider closestEnemy1 = null;
                Collider closestEnemy2 = null;
                float closestDistance1 = Mathf.Infinity;
                float closestDistance2 = Mathf.Infinity;

                foreach (Collider enemy in enemies)
                {
                    float distance = Vector3.Distance(transform.position, enemy.transform.position);
                    if (distance < closestDistance1)
                    {
                        closestEnemy2 = closestEnemy1;
                        closestDistance2 = closestDistance1;
                        closestEnemy1 = enemy;
                        closestDistance1 = distance;
                    }
                    else if (distance < closestDistance2)
                    {
                        closestEnemy2 = enemy;
                        closestDistance2 = distance;
                    }
                }
                ShootBullet(leftHand, closestEnemy1.transform.position);
                ShootBullet(rightHand, closestEnemy2.transform.position);
            }
            else if (enemies.Length == 1)
            {
                ShootBullet(leftHand, enemies[0].transform.position);
                ShootBullet(rightHand, enemies[0].transform.position);
            }
            nextFireTime = Time.time + fireRate;
        }
        
    }

    void ShootBullet(Transform hand, Vector3 targetPosition)
    {
        GameObject bullet = BulletPool.instance.GetPooledBullets();
        if (bullet != null)
        {
            PlayerMovement playerMovement = GetComponent<PlayerMovement>();
            playerMovement.SetTargetRotation(targetPosition);
            bullet.transform.position = hand.position;
            Vector3 direction = (targetPosition - hand.position).normalized;
            Bullet bulletScript = bullet.GetComponent<Bullet>();
            bulletScript.SetDirection(direction);
            bullet.SetActive(true);
        }
    }
}





