using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Fader : MonoBehaviour
{
    bool _enabled = false;
    public static Fader instance;

    public CanvasGroup faderCG;

    void Awake()
    {
        faderCG = this.gameObject.GetComponent<CanvasGroup>();
    }

    // Start is called before the first frame update
    void Start()
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

    void OnEnable()
    {
        if (!_enabled)
        {
            faderCG.alpha = 1;
            FadeEnable(0, 0.5f, false, 0);
            _enabled = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FadeEnable(float alpha, float time, bool changeScene, int sceneIndex)
    {
        LeanTween.alphaCanvas(faderCG, alpha, time).setEaseInOutCubic().setOnComplete( () => {
            if (changeScene)
            {
                SceneManager.LoadSceneAsync(sceneIndex);
            }
            else
            {
                this.gameObject.SetActive(false);
            }
        });
    }

    public void GoToBattle()
    {
        faderCG.alpha = 0;
        UiManager.instance.gameObjects[3].SetActive(true);
        FadeEnable(1, 0.5f, true, 1);
    }
}
