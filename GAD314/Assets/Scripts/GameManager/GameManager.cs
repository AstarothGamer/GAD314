using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private PlayerDataSO playerSO;
    // [SerializeField] private GameObject player;
    
    public bool savedSO;

    void Awake()
    {
        if(!playerSO.saved)
        {
            playerSO.ResetData();
        }

        StartCoroutine(Loading());

        Time.timeScale = 1f;
    }
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
                Time.timeScale = 0f;
                Cursor.lockState = CursorLockMode.None;
                menuPanel.SetActive(true);
            }
            else
            {
                Time.timeScale = 1f;
                Cursor.lockState = CursorLockMode.Locked;
                menuPanel.SetActive(false);
            }
        }
    }

    public void LoadLastSave()
    {
        SceneManager.LoadScene(2);
    }

    public void ReloadScene()
    {
        playerSO.ResetData();
        SceneManager.LoadScene(2);
    }
    
    public void ExitToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public IEnumerator Loading()
    {
        Time.timeScale = 0f;
        yield return new WaitForSeconds(2);
        Time.timeScale = 1f;
    }
}
