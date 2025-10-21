using UnityEngine;

public class DamageToPlayer : MonoBehaviour
{
    public int damage;

    public void OnTriggerEnter(Collider other)
    {
        var target = other.GetComponent<PlayerHealth>();

        if (target != null)
        {
            target.Damage(damage);
        }
        else
        {
            //play sound of hit wall or whatever
        }
        Destroy(gameObject);
    }
}
