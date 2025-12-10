using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{
    public GameObject dialogueBox;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    public float textSpeed = 0.05f;
    public int nextSceneIndex;

    private Dialogue currentDialogue;
    private int currentLineIndex;
    private bool isDialogueActive;
    private bool isTyping;
    private Coroutine typingCoroutine;


   
    void Start()
    {
        
    }

    void Update()
    {
        if (isDialogueActive && Input.GetMouseButtonDown(0))
        {
            HandleMouseClick();
        }
    }

    void HandleMouseClick()
    {
        if (isTyping)
        {
            FinishTyping();
        }
        else
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

        nameText.text = currentLine.characterName;
        typingCoroutine = StartCoroutine(TypeText(currentLine.dialogueText));

        currentLineIndex++;
    }

    IEnumerator TypeText(string text)
    {
        isTyping = true;
        dialogueText.text = "";

        foreach (char letter in text.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(textSpeed);
        }

        isTyping = false;
    }

    void FinishTyping()
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }

        Dialogue.DialogueLine currentLine = currentDialogue.dialogueLines[currentLineIndex - 1];
        dialogueText.text = currentLine.dialogueText;
        isTyping = false;
    }

    void EndDialogue()
    {
        isDialogueActive = false;
        dialogueBox.SetActive(false);
        nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            // SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            nextSceneIndex = 0;
        }

        SceneManager.LoadScene(nextSceneIndex);
    }
}

