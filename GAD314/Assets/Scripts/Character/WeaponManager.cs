using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] private GameObject mainGun;
    [SerializeField] private GameObject sword;
    [SerializeField] private PlayerDataSO playerSO;

    public bool katana;
    public bool gun;
    public bool grapplingGun;
    // [SerializeField] private GameObject grenadeLouncher;

    void Awake()
    {
        gun = playerSO.gun;
        katana = playerSO.katana;
        grapplingGun = playerSO.grapplingGun;
    }

    // Update is called once per frame
    void Update()
    {
        WeaponSwitch();
    }
    
    private void WeaponSwitch()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            mainGun.SetActive(true);
            sword.SetActive(false);
            // grenadeLouncher.SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            mainGun.SetActive(false);
            sword.SetActive(true);
            // grenadeLouncher.SetActive(false);
        }
        // else if(Input.GetKeyDown(KeyCode.Alpha3))
        // {
        //     mainGun.SetActive(false);
        //     sword.SetActive(false);
        //     grenadeLouncher.SetActive(true);
        // }
    }
}
