using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MediumEnemy : EnemyManager,IDamageable
{
    private int _mediumHealth;
    private void OnEnable()
    {
        _mediumHealth = enemyData.enemyHealth;
        
    }
    
    public void TakeDamage()
    {
        //damage here.
        
    }

    private void OnDisable()
    {
        //death fx and stuff.
    }
}
