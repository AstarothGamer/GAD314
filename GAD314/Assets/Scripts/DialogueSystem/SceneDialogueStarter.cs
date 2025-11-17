using UnityEngine;

public class SceneDialogueStarter : MonoBehaviour
{
    public Dialogue dialogueToPlay;
    public DialogueManager dialogueManager;

    void Start()
    {
        if (dialogueToPlay != null && dialogueManager != null)
        {
            dialogueManager.StartDialogue(dialogueToPlay);
        }
    }
}
