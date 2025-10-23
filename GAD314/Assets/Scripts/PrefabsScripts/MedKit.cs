using UnityEngine;

public class MedKit : MonoBehaviour
{
    public int healAmount;
    void Start()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        var target = other.GetComponent<PlayerHealth>();

        if (target != null)
        {
            target.Heal(healAmount);
            Destroy(gameObject);
            //Here add some sound for healling from medical kit
        }
    }
}
