using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void ShootScene()
    {
        SceneManager.LoadScene(2);
    }

    public void GrapplingScene()
    {
        SceneManager.LoadScene(1);
    }

    public void TimeStopScene()
    {
        SceneManager.LoadScene(3);
    }
    
    public void Exit()
    {
        Application.Quit();
    }
}
