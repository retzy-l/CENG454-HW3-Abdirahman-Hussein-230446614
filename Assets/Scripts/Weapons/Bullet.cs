// Bullet.cs
// CENG 454 – HW3: Core Breach
// Author: ABDIRAHMAN HUSSEIN | Student ID: 230446614
using UnityEngine;

public class Bullet : MonoBehaviour, IPoolable
{
    [SerializeField] private float speed = 20f;
    [SerializeField] private float lifetime = 3f;
    [SerializeField] private float damage = 10f;

    private ObjectPool sourcePool;
    private float timer;
    private Vector3 direction;

    public void Init(Vector3 dir, ObjectPool pool)
    {
        direction = dir.normalized;
        sourcePool = pool;
    }

    public void OnSpawn()
    {
        timer = 0f;
    }

    public void OnReturnToPool()
    {
        // Reset state before returning
        direction = Vector3.zero;
        timer = 0f;
    }

    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
        timer += Time.deltaTime;
        if (timer >= lifetime)
            ReturnToPool();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet")) return;

        IDamageable damageable = other.GetComponentInParent<IDamageable>();
        if (damageable != null)
        {
            damageable.TakeDamage(damage);

            
            if (AudioManager.Instance != null)
                AudioManager.Instance.PlayBulletHit();

            ReturnToPool();
        }
    }

    private void ReturnToPool()
    {
        if (sourcePool != null)
            sourcePool.Return(gameObject);
        else
            gameObject.SetActive(false);
    }
}