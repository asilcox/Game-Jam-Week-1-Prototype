using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemy : MonoBehaviour
{

    public int maxHealth = 100;
    public int currentHealth;

    public EnemyHealthBar healthBar;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = 0;
        healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(20);
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            GiveHealth(5);
        }
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;

        healthBar.SetHealth(currentHealth);
    }

    void GiveHealth(int regen)
    {
        currentHealth += regen;

        healthBar.SetHealth(currentHealth);
    }
}
