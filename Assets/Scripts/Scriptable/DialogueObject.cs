using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterDialogue", menuName = "Dialogue System/DialogueObject", order = 1)]
public class DialogueObject : ScriptableObject
{
    public string charName;
    public string[] dialogue;

    public bool isThereNextDialogue = false;
    public DialogueObject nextDialogue;
}
