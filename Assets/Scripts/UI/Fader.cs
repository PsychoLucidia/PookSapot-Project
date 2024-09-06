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
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        faderCG = this.gameObject.GetComponent<CanvasGroup>();
    }

    // Start is called before the first frame update
    void Start()
    {

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

    /// <summary>
    /// Fades the canvas group to the specified alpha value over the specified time.
    /// If changeScene is true, the scene with the specified index will be loaded.
    /// </summary>
    /// <param name="alpha">The target alpha value of the canvas group.</param>
    /// <param name="time">The time for the tweening effect.</param>
    /// <param name="changeScene">Whether or not to change the scene.</param>
    /// <param name="sceneIndex">The index of the scene to be loaded.</param>
    public void FadeEnable(float alpha, float time, bool changeScene, int sceneIndex)
    {
        // Cancel any existing tween
        FadeCancelTween();

        // Tween the alpha value of the canvas group
        LeanTween.alphaCanvas(faderCG, alpha, time).setEaseInOutCubic().setOnComplete( () => 
        {
            // If we should change the scene, load the scene with the specified index
            if (changeScene)
            {
                PookSceneManager.instance.LoadScene(sceneIndex);
            }
            // Otherwise, simply disable this object
            else
            {
                this.gameObject.SetActive(false);
            }
        });
    }

    public void GoToBattle(int sceneIndex)
    {
        faderCG.alpha = 0;
        FadeEnable(1, 0.5f, true, sceneIndex);
    }

    void FadeCancelTween()
    {
        LeanTween.cancel(this.gameObject);
    }
}
