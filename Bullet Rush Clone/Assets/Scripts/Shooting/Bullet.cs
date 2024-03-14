using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private TrailRenderer _bulletTrail;
    [SerializeField] private Rigidbody _bulletRb;
    [SerializeField] public float _bulletSpeed;
    [SerializeField] public float _bulletLifetime = 5f; // The bullet will deactivate after 5 seconds

    private Vector3 direction;

    private void Awake()
    {
        _bulletRb = GetComponent<Rigidbody>();
        _bulletTrail = GetComponent<TrailRenderer>();
    }

    public void SetDirection(Vector3 direction)
    {
        this.direction = direction;
    }

    private void OnEnable()
    {
        // Deactivate the bullet after a certain amount of time
        StartCoroutine(DeactivateAfterTime(_bulletLifetime));
    }

    private void OnDisable()
    {
        _bulletTrail.Clear();
        StopAllCoroutines();
    }

    private void Update()
    {
        transform.Translate(direction * _bulletSpeed * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 7 || other.gameObject.layer==8 )
        {
            var damageable = other.gameObject.GetComponent<IDamageable>();
            damageable?.TakeDamage();
            gameObject.SetActive(false);
        }
    }

    private IEnumerator DeactivateAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        gameObject.SetActive(false);
    }
}