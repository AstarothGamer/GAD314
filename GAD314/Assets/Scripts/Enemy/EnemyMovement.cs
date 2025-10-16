using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public List<GameObject> points = new List<GameObject>();
    public float speed = 2f;
    public int currentIndex = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (points.Count < 2) return;

        Vector3 target = points[currentIndex].transform.position;

        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        if(Vector3.Distance(transform.position, target) <= speed * Time.deltaTime)
        {
            if (currentIndex == 1)
            {
                currentIndex = 0;
            }
            else
            {
                currentIndex = 1;
            }
        }
    }
}
