using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class BulletPool : MonoBehaviour
{
    public static BulletPool instance;
    private List<GameObject> pooledBullets = new List<GameObject>();
    public int amounToPool = 30;
    [SerializeField] private Transform bulletPool;
    [SerializeField] private GameObject _bulletPrefab;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        for (int i = 0; i < amounToPool; i++)
        {
            GameObject bullet = Instantiate(_bulletPrefab, bulletPool);
            bullet.SetActive(false);
            pooledBullets.Add(bullet);
            
        }
        
    }

    public GameObject GetPooledBullets()
    {
        for (int i = 0; i < pooledBullets.Count; i++)
        {
            if (!pooledBullets[i].activeInHierarchy)
            {
                return pooledBullets[i];
            }
            
        }

        return null;
    }
}
