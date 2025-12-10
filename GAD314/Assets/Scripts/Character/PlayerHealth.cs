using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    // [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject deathPanel;
    [SerializeField] private GameObject UI;
    [SerializeField] private int maxHealth = 100;
    [SerializeField] public int currentHealth;
    [SerializeField] private TMP_Text healthText;

    [SerializeField] private PlayerDataSO playerSO;
    [SerializeField] private GameObject DamagePanel;



    // void Awake()
    // {
    //     currentHealth = maxHealth;
    //     healthText.text = currentHealth.ToString();        
    // }
    void Start()
    {
        currentHealth = playerSO.hp;
        healthText.text = currentHealth.ToString();
    }



    public void Damage(int damage)
    {
        currentHealth -= damage;
        //Here should be added sound of getting damage
        //below as an example how to use function to play a sound once
        AudioManager.Instance.PlaySoundAtPoint("Player Hit 1", transform.position);
        // in transform just add transform.position , because it should be played at players position
        // ot you can add it into script DamageToPlayer when it calls Damage(), if we want to play sound at the accurate place where player gets hit
        if (currentHealth <= 0) StartCoroutine(Die());

        Debug.Log("Player got " + damage + " damage. Now you have " + currentHealth + " hp");
        healthText.text = currentHealth.ToString();
        float a = 1f - (currentHealth * 1f / maxHealth);
        DamagePanel.GetComponent<Image>().color = new Color(1, 1, 1, a);
    }

    public void Heal(int amount)
    {
        currentHealth += amount;

        //Here can be added also a sound of healling, like a sigh of relief
        // or could be one of our voices, where one of us will say something like "I will not die today again"

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        healthText.text = currentHealth.ToString();
        Debug.Log("Healed to " + amount);
        float a = 1f - (currentHealth * 1f / maxHealth);
        DamagePanel.GetComponent<Image>().color = new Color(1, 1, 1, a);
    }
    
    private IEnumerator Die()
    {
        // Time.timeScale = 0f;
        //Play some visual and audio effect
        //for example bloody window, player falls, pertical effects of blood everywhere, sounds of hard breath 
        yield return new WaitForSecondsRealtime(1f);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        UI.SetActive(false);
        deathPanel.SetActive(true);
        //Here i'll add screen of death with loading from last saved moment once it will be ready
    }
}