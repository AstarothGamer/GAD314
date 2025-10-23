using UnityEngine;

public class Damageable : MonoBehaviour
{
    public float currentHealth;
    public float maxHealth = 100f;
    public bool isDead;

    void Awake()
    {
        currentHealth = maxHealth;
    }

    public virtual void Damage(float damage)
    {
        if (isDead || damage <= 0) return;

        currentHealth -= damage;
        //Play sound of hit

        if (currentHealth <= 0)
        {
            Die();
        }

        Debug.Log("enemy has " + currentHealth);
    }
    
    public virtual void Die()
    {
        //play sound and partickle effects of die
        isDead = true;
        gameObject.SetActive(false);
    }
}
