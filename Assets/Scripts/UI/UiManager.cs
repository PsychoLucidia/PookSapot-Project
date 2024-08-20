using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    public static UiManager instance;

    public GameObject[] gameObjects;
    public GameObject[] enableObject;

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
    }

    void Start()
    {
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
