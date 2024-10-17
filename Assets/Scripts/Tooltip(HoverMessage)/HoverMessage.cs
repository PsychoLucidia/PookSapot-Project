using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverMessage : MonoBehaviour
{
    public string message;

    public void OnMouseEnter()
    {
        Debug.Log("it works");
        TooltipManager._instance.SetAndShowToolTip(message);
    }

    public void OnMouseExit()
    {
        Debug.Log("cursor left the object");
        TooltipManager._instance.HideToolTip();  
    }
}
