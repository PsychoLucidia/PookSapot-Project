using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Fader : MonoBehaviour
{
    [SerializeField] bool _enabled = false;
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
            FadeCancelTween();

            _enabled = true;
        }


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FadeEnable(float alpha, float time, bool changeScene, int sceneIndex)
    {
        FadeCancelTween();

        LeanTween.alphaCanvas(faderCG, alpha, time).setEaseInOutCubic().setOnComplete( () => 
        {
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

    public void GoToBattle(int sceneIndex)
    {
        faderCG.alpha = 0;
        UiManager.instance.gameObjects[3].SetActive(true);
        FadeEnable(1, 0.5f, true, sceneIndex);
    }

    void FadeCancelTween()
    {
        LeanTween.cancel(this.gameObject);
    }
}
