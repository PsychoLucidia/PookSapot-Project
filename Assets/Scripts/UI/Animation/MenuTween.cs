using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuTween : MonoBehaviour
{
    public Transform menuTransform;
    public CanvasGroup menuCanvasGroup;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnEnable()
    {
        menuTransform.localScale = new Vector3(1.1f, 1.1f, 1);
        menuCanvasGroup.alpha = 0;

        MenuTweening();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MenuTweening()
    {
        LeanTween.scale(menuTransform.gameObject, new Vector3(1, 1, 1), 0.5f).setEaseOutExpo();
        LeanTween.alphaCanvas(menuCanvasGroup, 1, 0.5f).setEaseOutExpo();
    }
}
