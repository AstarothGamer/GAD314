using TMPro;
using UnityEngine;

public class QuestUI : MonoBehaviour
{
    //put the quest text from the canvas here
    public TMP_Text questText;

    // references to the player's weapons
    public WeaponManager weapon;


    void Update()
    {
        // If player has no katana active, the quest is to find it
        if (!weapon.katana)
        {
            questText.text = "Quest: Find a Katana";
        }
        // If katana is active but gun is not, next quest
        else if (weapon.katana && !weapon.gun)
        {
            questText.text = "Quest: Find a Gun";
        }
        else if (weapon.katana && weapon.gun && !weapon.grapplingGun)
        {
            questText.text = "Quest: Find a Grappling Gun";
        }
        // If both are active, quest is done
        else if (weapon.katana && weapon.gun && weapon.grapplingGun)
        {
            questText.text = "Quest: All weapons found!";
        }
    }
}