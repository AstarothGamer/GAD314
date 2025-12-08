using UnityEngine;

public class Potion : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerHealth>())
        {
            int healAmount = Random.Range(20, 30);
            other.GetComponent<PlayerHealth>().Heal(healAmount);
            AudioManager.Instance.PlaySoundAtPoint("Medkit_Pickup", transform.position);
            Destroy(gameObject);
        }
    }
}
