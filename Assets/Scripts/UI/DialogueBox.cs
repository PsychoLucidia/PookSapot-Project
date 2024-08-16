using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueBox : MonoBehaviour
{ 
    [Header("Data")]
    public static DialogueObject dialogueData;
    public Button invisibleButton;
    [SerializeField] Coroutine dialogueCoroutine;

    [Header("Animation Components")]
    public Transform boxOut;
    public Transform boxIn;
    public Transform nameBox;
    public Transform warnSign;
    public Transform exclamMark;
    public Transform line1;
    public Transform line2;


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
        if(DialogueBox.dialogueData != null)
        {
            DialogueAnimation();
            StartDialogue();
            ToggleButton(1);
            StartTextCoroutine();
        }   
    }

    // Update is called once per frame
    void Update()
    {
        // BoxDebug();
    }

    public void StartDialogue()
    {
        this.gameObject.SetActive(true);
        currentLine = 0;
        nameText.text = dialogueData.charName;
        dialogueText.text = null;
    }

    public void NextLine()
    {
        if (dialogueText.text.Length == dialogueData.dialogue[currentLine].Length)
        {
            if (dialogueData.isThereNextDialogue && currentLine == dialogueData.dialogue.Length - 1)
            {
                dialogueData = dialogueData.nextDialogue;
                StartDialogue();
                ChangeNameAnimation();
                StartTextCoroutine();
            }
            else
            {
                currentLine++;
                if (currentLine < dialogueData.dialogue.Length)
                    {   
                        Debug.Log("Next Line");
                        dialogueText.text = null;
                        StopCoroutine(dialogueCoroutine);
                        StartTextCoroutine();
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
            StopCoroutine(dialogueCoroutine);
            dialogueText.text = dialogueData.dialogue[currentLine];
        }
    }

    public void EndDialogue()
    {
        EndDialogueAnimation();
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

    void DialogueAnimation()
    {   
        CancelDialogueTween();

        boxOut.localScale = new Vector3(0, 0, 1);
        boxOut.localRotation = Quaternion.Euler(0, 0, -25);
        boxIn.localScale = new Vector3(0, 0, 1);
        boxIn.localRotation = Quaternion.Euler(0, 0, -25);
        nameBox.localPosition = new Vector3(-468, 0, 0);
        nameBox.localScale = new Vector3(0, 0, 1);

        // Animation for boxOut
        LeanTween.scaleX(boxOut.gameObject, 1.1f, 0.15f).setEaseOutCubic().setOnComplete(() => 
        {
            LeanTween.scaleX(boxOut.gameObject, 1, 0.05f).setEaseInCubic();
        });
        LeanTween.scaleY(boxOut.gameObject, 1.05f, 0.15f).setEaseOutCubic().setOnComplete(() => 
        {
            LeanTween.scaleY(boxOut.gameObject, 1, 0.05f).setEaseInCubic();
        });
        LeanTween.rotateZ(boxOut.gameObject, 2, 0.15f).setEaseOutCubic().setOnComplete(() => 
        {
            LeanTween.rotateZ(boxOut.gameObject, 0, 0.05f).setEaseInCubic();
        });

        // Animation for boxIn
        LeanTween.scaleX(boxIn.gameObject, 1.1f, 0.15f).setEaseOutCubic().setOnComplete(() => 
        {
            LeanTween.scaleX(boxIn.gameObject, 1, 0.05f).setEaseInCubic();
        });;
        LeanTween.scaleY(boxIn.gameObject, 1.05f, 0.15f).setEaseOutCubic().setOnComplete(() => 
        {
            LeanTween.scaleY(boxIn.gameObject, 1, 0.05f).setEaseInCubic();
        });
        LeanTween.rotateZ(boxIn.gameObject, 1, 0.15f).setEaseOutCubic().setOnComplete(() =>
        {
            LeanTween.rotateZ(boxIn.gameObject, 0, 0.05f).setEaseInCubic();
        });

        LeanTween.scaleX(nameBox.gameObject, 1, 0.3f).setEaseOutBounce().setDelay(0.1f);
        LeanTween.scaleY(nameBox.gameObject, 1, 0.3f).setEaseOutBounce().setDelay(0.1f);
    }

    void ChangeNameAnimation()
    {
        nameBox.localScale = new Vector3(0.8f, 0.8f, 1);
        nameBox.localPosition = new Vector3(-495, 15, 0);
        boxOut.localScale = new Vector3(0.95f, 0.95f, 1);
        boxIn.localScale = new Vector3(0.9f, 0.9f, 1);

        LeanTween.scaleX(nameBox.gameObject, 1, 0.3f).setEaseOutBounce();
        LeanTween.scaleY(nameBox.gameObject, 1, 0.3f).setEaseOutBounce();
        LeanTween.moveLocalX(nameBox.gameObject, -468, 0.3f).setEaseOutBounce();
        LeanTween.moveLocalY(nameBox.gameObject, 0, 0.3f).setEaseOutBounce();
        LeanTween.scaleX(boxOut.gameObject, 1, 0.3f).setEaseOutBounce();
        LeanTween.scaleY(boxOut.gameObject, 1, 0.3f).setEaseOutBounce();
        LeanTween.scaleX(boxIn.gameObject, 1, 0.3f).setEaseOutBounce();
        LeanTween.scaleY(boxIn.gameObject, 1, 0.3f).setEaseOutBounce();
    }

    void EndDialogueAnimation()
    {
        CancelDialogueTween();
        ToggleButton(0);

        boxOut.localScale = boxOut.localScale;
        boxOut.localRotation = boxOut.localRotation;
        boxIn.localScale = boxIn.localScale;
        boxIn.localRotation = boxIn.localRotation;
        nameBox.localPosition = nameBox.localPosition;
        nameBox.localScale = nameBox.localScale;

        // Animation for boxOut
        LeanTween.scaleX(boxOut.gameObject, 0, 0.15f).setEaseInCubic().setOnComplete(() =>
        {
            this.gameObject.SetActive(false);
        });
        LeanTween.scaleY(boxOut.gameObject, 0, 0.15f).setEaseInCubic();
        LeanTween.rotateZ(boxOut.gameObject, -20, 0.15f).setEaseInCubic();
        // Animation for boxIn
        LeanTween.scaleX(boxIn.gameObject, 0, 0.15f).setEaseInCubic();
        LeanTween.scaleY(boxIn.gameObject, 0, 0.15f).setEaseInCubic();
        LeanTween.rotateZ(boxIn.gameObject, -20, 0.15f).setEaseInCubic();

        LeanTween.scaleX(nameBox.gameObject, 0, 0.15f).setEaseInCubic();
        LeanTween.scaleY(nameBox.gameObject, 0, 0.15f).setEaseInCubic();
        LeanTween.moveLocalX(nameBox.gameObject, -524, 0.15f).setEaseInCubic();
        LeanTween.moveLocalY(nameBox.gameObject, 176, 0.15f).setEaseInCubic();
    }

    void StartTextCoroutine()
    {
        dialogueCoroutine = StartCoroutine(PlayText());
    }

    void ToggleButton(int index)
    {
        switch (index)
        {
            case 0:
                invisibleButton.interactable = false;
                break;
            case 1:
                invisibleButton.interactable = true;
                break;
        }
    }

    void CancelDialogueTween()
    {
        LeanTween.cancel(boxOut.gameObject);
        LeanTween.cancel(boxIn.gameObject);
        LeanTween.cancel(nameBox.gameObject);
    }
}