using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] private GameObject mainGun;
    [SerializeField] private GameObject sword;
    [SerializeField] private PlayerDataSO playerSO;

    public bool katana = false;
    public bool gun = false;
    public bool grapplingGun;
    bool gunOn = false;
    bool katanaOn = false;
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
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if(gunOn)
            {
                mainGun.SetActive(false);
                gunOn = false;
            }
            else if (gun)
            {
                katanaOn = false;
                gunOn = true;
                mainGun.SetActive(true);
                sword.SetActive(false);
            }
            
            // grenadeLouncher.SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if(katanaOn)
            {
                katanaOn = false;
                sword.SetActive(false);
            }
            else if (katana)
            {
                gunOn = false;
                katanaOn = true;
                mainGun.SetActive(false);
                sword.SetActive(true);
            }
            
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
