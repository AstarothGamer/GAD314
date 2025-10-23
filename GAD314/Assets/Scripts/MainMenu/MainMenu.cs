using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void NewGame()
    {
        SceneManager.LoadScene(1);
    }
    public void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player")) 
        SceneManager.LoadScene(2);
        
    }
    public void Exit()
    {
        Application.Quit();
    }
}
