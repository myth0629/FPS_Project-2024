using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerH : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage) {
        currentHealth -= damage;
        Debug.Log("Player HP : " + currentHealth);
        if(currentHealth <= 0) {
            Die();
        }
    }

    void Die(){
        Debug.Log("Player Died!");
    }
}
