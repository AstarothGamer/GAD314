using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private PlayerDataSO playerSO;
    [SerializeField] private GameObject player;
    
    public bool savedSO;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        // player.transform.position = playerSO.playerPosition;
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

    public void LoadLastSave()
    {
        SceneManager.LoadScene(1);
    }

    public void ReloadScene()
    {
        playerSO.ResetData();
        SceneManager.LoadScene(1);
    }
    
    public void ExitToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
