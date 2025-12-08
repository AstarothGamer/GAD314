using TMPro;
using UnityEngine;

public class QuestUI : MonoBehaviour
{
    //put the quest text from the canvas here
    public TMP_Text questText;

    // references to the player's weapons
    public GameObject katanaObject;   
    public GameObject gunObject;      
    

    void Update()
    {
        // If player has no katana active, the quest is to find it
        if (!katanaObject.activeSelf)
        {
            questText.text = "Quest: Find a Katana";
        }
        // If katana is active but gun is not, next quest
        else if (katanaObject.activeSelf && !gunObject.activeSelf)
        {
            questText.text = "Quest: Find a Gun";
        }
        // If both are active, quest is done
        else if (katanaObject.activeSelf && gunObject.activeSelf)
        {
            questText.text = "Quest: All weapons found!";
        }
    }
}
