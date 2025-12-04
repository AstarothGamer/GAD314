using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    public bool activated = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivatePortal()
    {
        activated = true;
        transform.GetChild(1).GetComponent<Renderer>().enabled = true;
    }

    public void OnTriggerEnter(Collider other)
    {
        if(!activated) return;

        if(other.GetComponentInChildren<PlayerHealth>())
        {
            SceneManager.LoadScene(3);
        }
    }
}
