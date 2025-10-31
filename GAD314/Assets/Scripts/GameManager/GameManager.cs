using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject menuPanel;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (menuPanel.activeSelf == false)
            {
                Cursor.lockState = CursorLockMode.None;
                menuPanel.SetActive(true);
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                menuPanel.SetActive(false);
            }
        }
    }
    
    public void ExitToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
