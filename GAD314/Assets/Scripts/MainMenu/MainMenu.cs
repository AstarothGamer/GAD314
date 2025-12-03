using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartingScene()
    {
        SceneManager.LoadScene(0);
    }
    
    public void Exit()
    {
        Application.Quit();
    }
}
