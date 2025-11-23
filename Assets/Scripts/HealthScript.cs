using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthScript : MonoBehaviour,IDamageble
{
    [SerializeField]private float health;

    [SerializeField] private float maxHealth;
    public float Health { get => health; set => health = value; }
    public float MaxHealth { get => maxHealth; set => maxHealth = value; }
    public bool isAlive = true;
    public void Die()
    {
        if (Health <= 0)
        {
            if(GetComponent<PlayerMovement>())
            {
                isAlive = false;
                GetComponent<PlayerMovement>().animator.Play("Die");
            }
            if (GetComponent<Enemy>())
            {
                isAlive = false;
                WavesManager wm = FindObjectOfType<WavesManager>();
                wm.OnEnemyKilled();
                GetComponent<Enemy>().animator.Play("Die");
                GetComponent<Enemy>().animator.transform.SetParent(null);
                Destroy(gameObject);    
            }
        }
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
