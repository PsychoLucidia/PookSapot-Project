using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Pointer Hovered");
    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Pointer Exited");
    }
}
