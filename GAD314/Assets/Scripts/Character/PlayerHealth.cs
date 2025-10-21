using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    [SerializeField] private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void Damage(int damage)
    {
        currentHealth -= damage;
        //Here should be added sound of getting damage
        //below as an example how to use function to play a sound once
        // AudioManager.Instance.PlaySoundAtPoint("gun-fire", firePoint.transform.position);
        // in transform just add transform.position , because it should be played at players position
        // ot you can add it into script DamageToPlayer when it calls Damage(), if we want to play sound at the accurate place where player gets hit
        if (currentHealth <= 0) StartCoroutine(Die());
    }
    
    private IEnumerator Die()
    {
        //Play some visual and audio effect
        //for example bloody window, player falls, pertical effects of blood everywhere, sounds of hard breath 
        yield return new WaitForSeconds(5f);
        //Here i'll add screen of death with loading from last saved moment once it will be ready
    }
}
