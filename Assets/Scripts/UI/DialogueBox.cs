using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueBox : MonoBehaviour
{ 
    [Header("Data")]
    public static DialogueObject dialogueData;

    [Header("Debug")]
    [SerializeField] int dialogueLength;
    [SerializeField] int boxTextLength;
    [SerializeField] int numOfDialogues;

    [Header("Settings")]
    public int currentLine = 0;
    public float textSpeed = 0.03f;

    [Header("Text Components")]
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;

    void Awake()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    void OnEnable()
    {
        StartDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        BoxDebug();
    }

    public void StartDialogue()
    {
        this.gameObject.SetActive(true);
        currentLine = 0;
        nameText.text = dialogueData.charName;
        dialogueText.text = null;
        StartCoroutine(PlayText());
    }

    public void NextLine()
    {
        if (dialogueText.text.Length == dialogueData.dialogue[currentLine].Length)
        {
            if (dialogueData.isThereNextDialogue && currentLine == dialogueData.dialogue.Length - 1)
            {
                dialogueData = dialogueData.nextDialogue;
                StartDialogue();
            }
            else
            {
                currentLine++;
                if (currentLine < dialogueData.dialogue.Length)
                    {   
                        Debug.Log("Next Line");
                        dialogueText.text = null;
                        StartCoroutine(PlayText());
                    }
                    else
                    {
                        Debug.Log("End Dialogue");
                        EndDialogue();
                    }
            }
        }
        else
        {
            StopAllCoroutines();
            dialogueText.text = dialogueData.dialogue[currentLine];
        }
    }

    public void EndDialogue()
    {
        this.gameObject.SetActive(false);
    }

    IEnumerator PlayText()
    {
        foreach (char letter in dialogueData.dialogue[currentLine].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void BoxDebug()
    {
        dialogueLength = dialogueData.dialogue[currentLine].Length;
        boxTextLength = dialogueText.text.Length;
        numOfDialogues = dialogueData.dialogue.Length;
    }
}