using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private int maxHealth = 100;
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

        Debug.Log("Player got " + damage + " damage. Now you have " + currentHealth + " hp");
    }

    public void Heal(int amount)
    {
        currentHealth += amount;

        //Here can be added also a sound of healling, like a sigh of relief
        // or could be one of our voices, where one of us will say something like "I will not die today again"

        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }
    
    private IEnumerator Die()
    {
        //Play some visual and audio effect
        //for example bloody window, player falls, pertical effects of blood everywhere, sounds of hard breath 
        yield return new WaitForSeconds(1f);
        // Cursor.lockState = CursorLockMode.None;
        menuPanel.SetActive(true);
        //Here i'll add screen of death with loading from last saved moment once it will be ready
    }
}
