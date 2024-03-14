using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData",menuName ="Enemy")]
public class EnemySO : ScriptableObject
{
    public  Material enemyMaterial;
    public int enemyHealth;
}
