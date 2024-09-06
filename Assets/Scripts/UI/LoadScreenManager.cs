using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadScreenManager : MonoBehaviour
{
    public static LoadScreenManager instance;

    public RectTransform loadBar;
    public RectTransform loadText;
    public CanvasGroup loadBarCanvasGroup;
    public CanvasGroup loadTextCanvasGroup;
    public CanvasGroup loadScreenCanvasGroup;

    public GameObject loadScreenObj;

    public Vector2 loadTextInitPos;
    public Vector2 loadBarInitPos;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }

        loadTextInitPos = loadText.localPosition;
        loadBarInitPos = loadBar.anchoredPosition;

        loadScreenObj.SetActive(false);
    }

    void OnEnable()
    {
        loadTextCanvasGroup.alpha = 0;
        loadBarCanvasGroup.alpha = 0;

        LeanTween.alphaCanvas(loadTextCanvasGroup, 1, 0.5f).setEaseOutCubic();
        LeanTween.alphaCanvas(loadBarCanvasGroup, 1, 0.5f).setEaseOutCubic();
    }
}
