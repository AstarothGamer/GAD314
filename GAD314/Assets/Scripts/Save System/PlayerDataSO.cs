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
    public bool saved;

    public int ammoCage;
    public int ammoReserve;

    public void ResetData()
    {
        playerPosition = new Vector3(776.192993f,6.98099995f,1439.78003f);
        hp = 100;
        gun = false;
        katana = false;
        grapplingGun = false;
        key = false;
        saved = false;

        ammoCage = 30;
        ammoReserve = 60;
    }
}

