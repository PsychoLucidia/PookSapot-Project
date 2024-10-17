using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using TMPro; // Import the TextMeshPro namespace

public class HoverButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    // The GameObject that will appear and disappear
    public GameObject hoverObject;

    // List of messages to choose from for click
    public List<string> clickMessages;

    // TextMeshPro component to display the selected messages
    public TextMeshProUGUI messageDisplay; // Use TextMeshProUGUI for UI text

    // Text to display when hovering
    public string hoverText; // Text to show when hovering over the button

    // Start is called before the first frame update
    void Start()
    {
        // Make sure the hoverObject is not visible at the start
        hoverObject.SetActive(false);
    }

    // Called when the pointer enters the Button
    public void OnPointerEnter(PointerEventData eventData)
    {
        // Show the hoverObject and set its text
        hoverObject.SetActive(true);
        UpdateHoverText();
    }

    // Called when the pointer exits the Button
    public void OnPointerExit(PointerEventData eventData)
    {
        // Hide the hoverObject
        hoverObject.SetActive(false);
    }

    // Called when the button is clicked
    public void OnPointerClick(PointerEventData eventData)
    {
        // Show the hoverObject
        hoverObject.SetActive(true);

        // Display a random message from the clickMessages list
    }

    // Method to update the hover text
    private void UpdateHoverText()
    {
        // Assuming the hoverObject has a TextMeshProUGUI component to display the hover text
        TextMeshProUGUI hoverTextComponent = hoverObject.GetComponentInChildren<TextMeshProUGUI>();
        if (hoverTextComponent != null)
        {
            hoverTextComponent.text = hoverText; // Set the hover text
        }
    }

    // Method to select and display a random message from clickMessages
    private void DisplayRandomClickMessage()
    {
        // Clear previous messages
        messageDisplay.text = "";

        // Select a random index
        int randomIndex = Random.Range(0, clickMessages.Count);

        // Display the selected message
        messageDisplay.text = clickMessages[randomIndex];
    }
}