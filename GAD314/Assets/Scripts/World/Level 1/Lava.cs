using UnityEngine;

public class Lava : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<PlayerHealth>();
        if(other.CompareTag("Player"))
        {
            player.Damage(100);
        }
    }
}
