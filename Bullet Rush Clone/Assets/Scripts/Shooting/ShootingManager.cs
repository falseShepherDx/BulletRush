using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingManager : MonoBehaviour
{
    public Transform leftHand;
    public Transform rightHand;
    public float shootingRange = 10f;
    public float fireRate = 0.5f;
    private float nextFireTime = 0f;
    [SerializeField] private float rotationSpeed;
    [SerializeField] public ParticleSystem magicExplosinParticle,magicAuraParticle;
    [SerializeField] private float  maxAuraScale;
    

    void Update()
    {
        
        if (Time.time > nextFireTime)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, shootingRange, LayerMask.GetMask("Enemy") | LayerMask.GetMask("LargeEnemy"));
            List<Collider> enemies = new List<Collider>();
            
            foreach (Collider collider in colliders)
            {
                if (collider.gameObject.activeInHierarchy)
                {
                    enemies.Add(collider);
                }
            }

            if (colliders.Length >= 2)
            {
                Collider closestEnemy = null;
                Collider farthestEnemy = null;
                float closestDistance = Mathf.Infinity;
                float farthestDistance = 0;

                foreach (Collider enemy in enemies)
                {
                    float distance = Vector3.Distance(transform.position, enemy.transform.position);
                    if (distance < closestDistance)
                    {
                        closestEnemy = enemy;
                        closestDistance = distance;
                    }
                    if (distance > farthestDistance)
                    {
                        farthestEnemy = enemy;
                        farthestDistance = distance;
                    }
                }
                
                ShootBullet(rightHand, closestEnemy.transform.position);
                ShootBullet(leftHand, farthestEnemy.transform.position);
            }
            else if (colliders.Length == 1)
            {
                ShootBullet(rightHand, enemies[0].transform.position);
                ShootBullet(leftHand, enemies[0].transform.position);
            }
            nextFireTime = Time.time + fireRate;
        }
        
        if (Input.GetMouseButton(0))
        {
            if (magicAuraParticle.transform.localScale.x < maxAuraScale)
            {
                magicAuraParticle.transform.localScale += Vector3.one * (Time.deltaTime);
            }
            
        }
        if (!Input.GetMouseButtonUp(0)) return;
        DestroyCircle();
        magicExplosinParticle.Play();
        magicAuraParticle.transform.localScale = default;
    }

    void ShootBullet(Transform hand, Vector3 targetPosition)
    {
        GameObject bullet = BulletPool.instance.GetPooledBullets();
        if (bullet != null)
        {
            RotateTowardsTarget(targetPosition);
            bullet.transform.position = hand.position;
            Vector3 direction = (targetPosition - hand.position).normalized;
            Bullet bulletScript = bullet.GetComponent<Bullet>();
            bulletScript.SetDirection(direction);
            bullet.SetActive(true);
        }
    }
    void RotateTowardsTarget(Vector3 targetPosition)
    {
        targetPosition.y = transform.position.y;
        Vector3 direction = (targetPosition - transform.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
    }
    private void DestroyCircle()
    {
        Collider[] colliders = new Collider[200];
        var results=Physics.OverlapSphereNonAlloc(transform.position, magicAuraParticle.transform.localScale.x,colliders,LayerMask.GetMask("Enemy"));
        for(var i=0;i<results;i++)
        {
            colliders[i].gameObject.SetActive(false);
        }
    }
}

