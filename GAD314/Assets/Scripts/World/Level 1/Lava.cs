using UnityEngine;

public class Lava : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<PlayerHealth>();
        if(other.includeLayers == 7)
        {
            player.Damage(100);
        }
    }
}
