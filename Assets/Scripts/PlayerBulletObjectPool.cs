using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletObjectPool : MonoBehaviour
{
    public static PlayerBulletObjectPool instance;

    public GameObject bulletPrefab;

    public int poolSize = 20;

    public List<GameObject> bullets = new List<GameObject>();

    private void Awake()
    {
        instance = this;
    }
    public Transform bulletParent;
    private void Start()
    {
        if (bulletParent == null)
        {
            GameObject parent = new GameObject("BulletPoolParent");
            bulletParent = parent.transform;
        }
        for (int i = 0; i < poolSize; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab, bulletParent);
            bullet.SetActive(false);
            bullets.Add(bullet);
        }
    }

    public GameObject GetBullet()
    {
        foreach (GameObject bullet in bullets)
        {
            if(!bullet.activeInHierarchy)
            {
                return bullet;
            }
        }

        GameObject newBullet = Instantiate(bulletPrefab);
        newBullet.SetActive(false);
        bullets.Add(newBullet);
        return newBullet;
    }


}
