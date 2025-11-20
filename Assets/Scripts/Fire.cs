using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    public float fireRate;
    public Transform firePoint;
    private float nextFire;

    void Update()
    {
        if (Time.time > nextFire && GetComponent<PlayerMovement>().target != null)
        {
            nextFire = Time.time + fireRate;
            Shoot();
        }
    }
    void Shoot()
    {
        GameObject bullet = PlayerBulletObjectPool.instance.GetBullet();
        bullet.transform.position = firePoint.position;
        bullet.transform.rotation = firePoint.rotation;
        bullet.SetActive(true);
    }
}
