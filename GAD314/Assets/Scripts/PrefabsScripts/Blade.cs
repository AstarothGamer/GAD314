using UnityEngine;

public class Blade : MonoBehaviour
{
    [SerializeField] private MeleeAttack attack;

    public void OnTriggerEnter(Collider other)
    {
        if(attack.isSwinging)
        {
            var damageable = other.GetComponent<Damageable>();
            if (damageable != null)
            {
                damageable.Damage(attack.damage);
                
            }
        }
    }
}
