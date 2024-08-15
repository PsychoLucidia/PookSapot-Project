using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UiManager : MonoBehaviour
{
    public virtual void OpenDialogue()
    {
        this.gameObject.SetActive(true);
    }

    public virtual void CloseDialogue()
    {
        this.gameObject.SetActive(false);
    }
}
