using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem.Haptics;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] private GameObject checkPointPanel;
    [SerializeField] PlayerDataSO playerSO;

    [SerializeField] WeaponManager weaponManager;
    [SerializeField] PlayerHealth playerHealth;
    [SerializeField] Shooting gun;
    Vector3 checkBox = new Vector3(1f, 1f, 1f);
    RaycastHit hit;

    void FixedUpdate()
    {
        // CheckingRaycast();
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
    }

    // private void CheckingRaycast()
    // {
    //     if (Physics.BoxCast(transform.position, checkBox, transform.forward, out hit))
    //     {
    //         if (hit.collider.name == "Player")
    //         {
    //             checkPointPanel.SetActive(true);
    //         }
    //     }
    //     else
    //     {
    //         checkPointPanel.SetActive(false);
    //     }
    // }

    public void OTriggerEnter(Collider other)
    {
        if (other.includeLayers == 7)
        {
            checkPointPanel.SetActive(true);
        }
    }
    public void OiggerExit(Collider other)
    {
        if (other.includeLayers == 7)
        {
            checkPointPanel.SetActive(false);
        }
    }
}
