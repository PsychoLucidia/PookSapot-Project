using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HoverButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    // The GameObject that will appear and disappear
    public GameObject hoverObject;

    // The Button that will trigger the hover effect
    public Button button;

    // Update is called once per frame
    void Start()
    {
        // Make sure the hoverObject is not visible at the start
        hoverObject.SetActive(false);
    }

    // Called when the pointer enters the Button
    public void OnPointerEnter(PointerEventData eventData)
    {
        // Show the hoverObject
        hoverObject.SetActive(true);
    }

    // Called when the pointer exits the Button
    public void OnPointerExit(PointerEventData eventData)
    {
        // Hide the hoverObject
        hoverObject.SetActive(false);
    }
}