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
        Debug.Log("Enemy got damage " + damage);
        //Play sound of hit

        if (currentHealth <= 0)
        {
            Die();
        }
    }
    
    public virtual void Die()
    {
        //play sound and partickle effects of die
        isDead = true;
        gameObject.SetActive(false);
    }
}
