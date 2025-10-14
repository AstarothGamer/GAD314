using System.Collections;
using UnityEngine;

public class TimeStop : MonoBehaviour
{
    private bool coolDown = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            if(coolDown == false)
            {
                StartCoroutine(TimeStopCoroutine());
            }
        }
    }

    private IEnumerator TimeStopCoroutine()
    {
        coolDown = true;
        Time.timeScale = 0.5f;
        yield return new WaitForSeconds(5);
        Time.timeScale = 1f;
        StartCoroutine(CoolDown());
    }
    
    private IEnumerator CoolDown()
    {
        yield return new WaitForSeconds(20);
        coolDown = false;
    }
}
