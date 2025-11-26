using UnityEngine;

public class Drops : MonoBehaviour
{
    [SerializeField] private GameObject ammos;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void Drop(Transform transform)
    {
        int r = Random.Range(0,101);

        if(r > 50)
        {
            Instantiate(ammos, transform.position, Quaternion.identity);
        }
    }

}
