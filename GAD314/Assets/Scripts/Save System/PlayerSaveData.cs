using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "PlayerData", order = 1)]
public class PlayerDataSO : ScriptableObject
{
    public Vector3 playerPosition;
    public int hp;
    public bool gun;
    public bool katana;
    public bool grapplingGun;
    public bool key;

    public int ammoCage;
    public int ammoReserve;

    public void ResetData()
    {
        playerPosition = Vector3.zero;
        hp = 0;
        gun = false;
        katana = false;
        grapplingGun = false;
        key = false;

        ammoCage = 0;
        ammoReserve = 0;
    }
}

