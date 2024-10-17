using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonMessageHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public static ButtonMessageHover _instance;

    public GameObject hoverPanel;
    public void OnPointerEnter(PointerEventData eventData)
    {
        hoverPanel.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        hoverPanel.SetActive(false);
    }
}
