using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthScript : MonoBehaviour,IDamageble
{
    [SerializeField]private float health;

    [SerializeField] private float maxHealth;
    public float Health { get => health; set => health = value; }
    public float MaxHealth { get => maxHealth; set => maxHealth = value; }

    public void Die()
    {
        if (Health <= 0)
            Destroy(gameObject);
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;
    }

    private void Start()
    {
        Health = MaxHealth;
    }

}
