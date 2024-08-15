using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueBox : MonoBehaviour
{ 
    [Header("Data")]
    public DialogueObject dialogueData;

    [Header("Settings")]
    public int currentLine = 0;
    public float textSpeed = 0.1f;

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

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            
        }
    }

    void StartDialogue()
    {
        this.gameObject.SetActive(true);
        currentLine = 0;

    }

    IEnumerator PlayText()
    {
        foreach (char letter in dialogueData.dialogue[currentLine].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(textSpeed);
        }
    }
}