using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] private GameObject mainGun;
    [SerializeField] private GameObject sword;
    [SerializeField] private GameObject grapGun;

    [SerializeField] private GameObject mainGunUI;
    [SerializeField] private GameObject swordUI;
    [SerializeField] private GameObject grapGunUI;

    [SerializeField] private PlayerDataSO playerSO;
    [SerializeField] private Shooting gunScript;
    [SerializeField] private MeleeAttack swordScript;
    [SerializeField] private GrapplingGun grapScript;
    

    public bool katana = false;
    public bool gun = false;
    public bool grapplingGun = false;

    public bool grapGunOn = false;
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
                mainGunUI.GetComponent<Image>().color = new Color32(130, 21, 14, 25);
                gunOn = false;
            }
            else if (gun)
            {
                katanaOn = false;
                gunOn = true;
                mainGunUI.GetComponent<Image>().color = new Color32(255, 20, 0, 90);
                swordUI.GetComponent<Image>().color = new Color32(130, 21, 14, 25);
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
                swordUI.GetComponent<Image>().color = new Color32(130, 21, 14, 25);
                sword.SetActive(false);
            }
            else if (katana && !swordScript.isSwinging)
            {
                gunOn = false;
                katanaOn = true;
                mainGunUI.GetComponent<Image>().color = new Color32(130, 21, 14, 25);
                swordUI.GetComponent<Image>().color = new Color32(255, 20, 0, 90);
                mainGun.SetActive(false);
                sword.SetActive(true);
            }
            
            // grenadeLouncher.SetActive(false);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            if(grapGunOn)
            {
                grapGunOn = false;
                grapGunUI.GetComponent<Image>().color = new Color32(130, 21, 14, 25);
                grapGun.SetActive(false);
            }
            else if(grapplingGun && !grapScript.usingGrappling)
            {
                grapGunOn = true;
                grapGunUI.GetComponent<Image>().color = new Color32(255, 20, 0, 90);
                grapGun.SetActive(true);
            }
            
        }
    }

    public void GettingAmmos(int amount)
    {
        gunScript.ammoReserve += amount;

        if(gunScript.ammoReserve > 360)
        {
            gunScript.ammoReserve = 360;
        }
    }
}
