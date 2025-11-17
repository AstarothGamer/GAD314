using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem.Haptics;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] private GameObject checkPointPanel;
    // [SerializeField] private GameObject gunPickUp;
    // [SerializeField] private GameObject swordPickUp;
    [SerializeField] PlayerDataSO playerSO;

    [SerializeField] WeaponManager weaponManager;
    [SerializeField] PlayerHealth playerHealth;
    [SerializeField] Shooting gun;
    [SerializeField] Camera cam;
    [SerializeField] private bool inArea = false;

    void FixedUpdate()
    {
        CheckingRaycast();
        
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            if(inArea == true)
            {
                SaveData();
            }
        }
    }

    public void SaveData()
    {
        playerSO.playerPosition = playerHealth.transform.position;
        playerSO.hp = playerHealth.currentHealth;

        playerSO.gun = weaponManager.gun;
        playerSO.katana = weaponManager.katana;
        playerSO.grapplingGun = weaponManager.grapplingGun;

        playerSO.ammoCage = gun.ammoCage;
        playerSO.ammoReserve = gun.ammoReserve;

        Debug.Log("Data saved");
    }

    private void CheckingRaycast()
    {
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 4f))
        {
            if (hit.collider.CompareTag("CheckPoint"))
            {
                checkPointPanel.SetActive(true);
                inArea = true;
            }
        
        }
        else
        {
            checkPointPanel.SetActive(false);
            inArea = false;
        }
    }
}
