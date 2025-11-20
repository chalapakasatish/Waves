using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float speed;
    public float lifeTime;

    private void OnEnable()
    {
        Invoke(nameof(Deactivate), lifeTime);
    }

    void Deactivate()
    {
        gameObject.SetActive(false);
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
    private void OnDisable()
    {
        CancelInvoke();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Deactivate();
            IDamageble nObject = collision.gameObject.GetComponent<IDamageble>();
            nObject.TakeDamage(10);
            nObject.Die();
        }
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Deactivate();
        }
    }
}
