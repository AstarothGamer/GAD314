using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private int damage = 20;
    private int speed = 30;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(TimeLife());
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    private IEnumerator TimeLife()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }

    void Movement()
    {
        Vector3 forward = transform.forward;

        forward.Normalize();

        Vector3 movement = forward * speed * Time.deltaTime;
        transform.position += movement;
    }
    
    public void OnTriggerEnter(Collider other)
    {
        var target = other.GetComponent<Damageable>();

        if (target != null)
        {
            target.Damage(damage);
        }
        else
        {
            //play sound of hit wall or whatever
        }

        if (other.CompareTag("BulletIgnore"))
        {
                
        }
        else
        {
            Destroy(gameObject);
        }
        
    }
}
