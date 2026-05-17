// BaseWeapon.cs
// CENG 454 – HW3: Core Breach
// Author: ABDIRAHMAN HUSSEIN | Student ID: 230446614
using UnityEngine;

public class BaseWeapon : MonoBehaviour, IWeapon
{
    [SerializeField] protected float damage = 10f;
    [SerializeField] protected float fireRate = 0.5f;
    [SerializeField] protected Transform firePoint;
    [SerializeField] protected ObjectPool bulletPool;

    protected float fireTimer;

    public virtual void Fire()
    {
        if (fireTimer > 0) return;
        fireTimer = 1f / fireRate;

        GameObject bulletObj = bulletPool.Get();
        bulletObj.transform.position = firePoint.position;
        bulletObj.transform.rotation = firePoint.rotation;

        Bullet bullet = bulletObj.GetComponent<Bullet>();
        if (bullet != null)
            bullet.Init(firePoint.forward, bulletPool);

        if (AudioManager.Instance != null)
            AudioManager.Instance.PlayShoot();
    }

    public virtual float GetDamage() => damage;
    public virtual float GetFireRate() => fireRate;

    void Update()
    {
        if (fireTimer > 0)
            fireTimer -= Time.deltaTime;
    }
}