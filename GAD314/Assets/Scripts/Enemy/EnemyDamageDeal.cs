using UnityEngine;

public class EnemyDamageDeal : MonoBehaviour
{
    [SerializeField] private MeleeEnemy enemy;

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if(enemy.isAttacking)
        {
            if(other.CompareTag("Player"))
            {
                other.GetComponent<PlayerHealth>().Damage(enemy.damage);
            }
        }
    }
}
