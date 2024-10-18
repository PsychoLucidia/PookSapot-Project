using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuTween : MonoBehaviour
{
    public BGTween bgTween;
    public Transform menuTransform;
    public Transform sep1, sep2, sep3; // UI comic separators
    public CanvasGroup menuCanvasGroup;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnEnable()
    {
        menuTransform.localScale = new Vector3(1.1f, 1.1f, 1);
        sep1.localPosition = new Vector3(-42, 1076, 0);
        sep2.localPosition = new Vector3(1325, 407, 0);
        sep3.localPosition = new Vector3(-1658, 96.652f, 0);
        menuCanvasGroup.alpha = 0;

        MenuTweening();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MenuTweening()
    {
        bgTween.Scale(1f, 0.5f, 1);
        LeanTween.scale(menuTransform.gameObject, new Vector3(1, 1, 1), 0.5f).setEaseOutExpo();

        LeanTween.moveLocal(sep1.gameObject, new Vector3(265, 0, 0), 0.5f).setEaseOutExpo();
        LeanTween.moveLocal(sep2.gameObject, new Vector3(634, 57, 0), 0.5f).setEaseOutExpo().setDelay(0.1f);
        LeanTween.moveLocal(sep3.gameObject, new Vector3(-314.48f, -112, 0), 0.5f).setEaseOutExpo().setDelay(0.2f);

        LeanTween.alphaCanvas(menuCanvasGroup, 1, 0.5f).setEaseOutExpo();
    }
}
