using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    [Header("Singleton")]
    public static UiManager instance;

    [Header("Game Objects")]
    public GameObject[] gameObjects;
    public GameObject[] enableObject;

    [Header("Fader")]
    public GameObject faderObj;
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

        foreach (GameObject obj in gameObjects)
        {
            obj.SetActive(false);
        }

        faderObj.SetActive(false);
    }

    void Start()
    {
        faderObj.SetActive(true);


        foreach (GameObject obj in enableObject)
        {
            obj.SetActive(true);
        }
    }

    public void ActivateObject(int index)
    {
        gameObjects[index].SetActive(true);
    }
}
