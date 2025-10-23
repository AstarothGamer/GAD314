using UnityEngine;

public class Sword : MonoBehaviour
{
    [SerializeField] private MeleeAttack attack;

    public void OnTriggerEnter(Collider other)
    {
        if( attack.isSwinging == true)
        {
            var damageable = other.GetComponent<Damageable>();
            if (damageable != null)
            {
                damageable.Damage(attack.damage);
            }
        }
    }
}
