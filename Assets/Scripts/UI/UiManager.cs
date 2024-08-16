using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    public static UiManager instance;

    public GameObject[] dialogueBox;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        foreach (GameObject obj in dialogueBox)
        {
            obj.SetActive(false);
        }
    }

    public void ActivateObject(int index)
    {
        dialogueBox[index].SetActive(true);
    }
}
