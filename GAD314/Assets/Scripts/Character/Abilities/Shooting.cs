using System.Collections;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject firePoint;
    private float timer = 0.2f;
    // [SerializeField] 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (Input.GetKey(KeyCode.Mouse0))
        {
            if (timer < 0.2f) return;
            Instantiate(bulletPrefab, firePoint.transform.position, firePoint.transform.rotation);
            timer = 0f;
        }
    }


}
