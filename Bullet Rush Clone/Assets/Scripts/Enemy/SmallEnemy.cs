using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallEnemy : EnemyManager,IDamageable
{
    private int smallHealth;
    public GameObject deathParticlePrefab;
    private ParticleSystem _particleSystem;

    private void OnEnable()
    {
        smallHealth = enemyData.enemyHealth;
        _particleSystem = GetComponent<ParticleSystem>();
    }
    
    public void TakeDamage()
    {
        GetHit();
        _particleSystem.Play();
        smallHealth--;
        if (smallHealth <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    void GetHit()
    {
        transform.localScale += Vector3.one * 0.03f;
        transform.position += Vector3.down * 0.03f;  
    }
    private void OnDisable()
    {
        var particle = Instantiate(deathParticlePrefab, transform.position, Quaternion.identity);
        particle.SetActive(true);
        Destroy(particle,1f);
    }
}
