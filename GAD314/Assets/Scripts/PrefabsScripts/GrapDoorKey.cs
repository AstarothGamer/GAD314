using UnityEngine;

public class GrapDoorKey : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            other.GetComponent<Inventory>().PickUp();
            Destroy(gameObject);
        }
    }
}
