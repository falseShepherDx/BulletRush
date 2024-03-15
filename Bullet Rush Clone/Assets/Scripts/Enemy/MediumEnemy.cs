using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MediumEnemy : EnemyManager,IDamageable
{
    private int _mediumHealth;
    public GameObject deathParticlePrefab;
    private ParticleSystem _particleSystem;
    private void OnEnable()
    {
        _mediumHealth = enemyData.enemyHealth;
        _particleSystem = GetComponent<ParticleSystem>();

    }
    
    public void TakeDamage()
    {
        GetHit();
        _particleSystem.Play();
        _mediumHealth--;
        if (_mediumHealth <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnDisable()
    {
        //death fx and stuff.
        var particle = Instantiate(deathParticlePrefab, transform.position, Quaternion.identity);
        particle.SetActive(true);
        Destroy(particle,1f);
        
    }

    void GetHit()
    {
        transform.localScale += Vector3.one * 0.03f;
        transform.position += Vector3.down * 0.03f;  
    }
}
