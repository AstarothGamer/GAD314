using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public GameObject dialogueBox;
    public Image characterImage;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;

    private Dialogue currentDialogue;
    private int currentLineIndex;
    private bool isDialogueActive;

    void Start()
    {
        dialogueBox.SetActive(false);
    }

    void Update()
    {
        if (isDialogueActive && Input.GetKeyDown(KeyCode.Space))
        {
            DisplayNextLine();
        }
    }

    public void StartDialogue(Dialogue dialogue)
    {
        currentDialogue = dialogue;
        currentLineIndex = 0;
        isDialogueActive = true;
        dialogueBox.SetActive(true);
        DisplayNextLine();
    }

    void DisplayNextLine()
    {
        if (currentLineIndex >= currentDialogue.dialogueLines.Length)
        {
            EndDialogue();
            return;
        }

        Dialogue.DialogueLine currentLine = currentDialogue.dialogueLines[currentLineIndex];

        characterImage.sprite = currentLine.characterImage;
        nameText.text = currentLine.characterName;
        dialogueText.text = currentLine.dialogueText;

        currentLineIndex++;
    }

    void EndDialogue()
    {
        isDialogueActive = false;
        dialogueBox.SetActive(false);
    }
}
