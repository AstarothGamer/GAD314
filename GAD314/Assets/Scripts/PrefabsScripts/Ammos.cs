using UnityEngine;

public class Ammos : MonoBehaviour
{
    int amount;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        amount = Random.Range(15, 31);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            other.GetComponent<WeaponManager>().GettingAmmos(amount);
            Destroy(gameObject);
        }
    }
}
