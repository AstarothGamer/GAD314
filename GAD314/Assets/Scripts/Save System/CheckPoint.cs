using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public PlayerDataSO playerSO;

    WeaponManager weaponManager;
    PlayerHealth playerHealth;
    Shooting gun;


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
}
