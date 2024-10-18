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
    public bool isCharacter;

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
        AnimateBox("Open");
        UpdateHoverText();
    }

    // Called when the pointer exits the Button
    public void OnPointerExit(PointerEventData eventData)
    {
        AnimateBox("Close");
    }

    // Called when the button is clicked
    public void OnPointerClick(PointerEventData eventData)
    {
        // Display a random message from the clickMessages list
        if (isCharacter)
        {
            DisplayRandomClickMessage();
        }
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

    void AnimateBox(string index)
    {
        switch (index)
        {
            case "Open":
                hoverObject.SetActive(true);
                LeanTween.cancel(hoverObject.transform.gameObject);

                if (!hoverObject.activeSelf)
                {
                    hoverObject.transform.localScale = new Vector3(0f, 0f, 1f);
                    hoverObject.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
                }

                LeanTween.rotateZ(hoverObject.transform.gameObject, 7f, 0.2f).setEaseOutCirc();

                LeanTween.scale(hoverObject.transform.gameObject, new Vector3(1.1f, 1.1f, 1f), 0.1f).setEaseOutCirc().setOnComplete(() => {
                    LeanTween.scale(hoverObject.transform.gameObject, new Vector3(0.95f, 0.5f, 1f), 0.05f).setEaseInCirc().setOnComplete(() => {
                        LeanTween.scale(hoverObject.transform.gameObject, new Vector3(1f, 1f, 1f), 0.05f).setEaseOutCirc();
                    });
                });
                break;
            case "Close":
                LeanTween.cancel(hoverObject.transform.gameObject);

                LeanTween.rotateZ(hoverObject.transform.gameObject, 0f, 0.2f).setEaseInCirc();
                LeanTween.scale(hoverObject.transform.gameObject, new Vector3(0f, 0f, 1f), 0.1f).setEaseInCirc().setOnComplete(() => {
                    hoverObject.SetActive(false);
                });
                break;
            default:
                break;
        }
    }

}