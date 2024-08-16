using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractDialogue : MonoBehaviour
{
    public virtual void Interact(DialogueObject dialogueObject)
    {
        DialogueBox.dialogueData = dialogueObject;
    }

    public virtual void GameObjectIndex(int index)
    {
        UiManager.instance.ActivateObject(index);
    }
}
