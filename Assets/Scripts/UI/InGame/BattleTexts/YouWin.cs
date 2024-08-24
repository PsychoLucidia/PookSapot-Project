using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YouWin : MonoBehaviour
{
    public Transform backTransform;
    public CanvasGroup backCG;
    public Transform textTransform;
    public CanvasGroup textCG;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnEnable()
    {
        backTransform.localScale = new Vector3(0, 0, 1);
        textTransform.localScale = new Vector3(1.2f, 1.2f, 1);
        textCG.alpha = 0;

        AnimateWin();
    }

    public void AnimateWin()
    {

        LeanTween.scale(backTransform.gameObject, new Vector3(1.2f, 1.2f, 1), 0.2f).setEaseOutCubic().setOnComplete(() => {
            LeanTween.scale(backTransform.gameObject, new Vector3(1f, 1f, 1), 0.2f).setEaseOutBounce();
            LeanTween.scale(textTransform.gameObject, new Vector3(1f, 1f, 1), 0.5f).setEaseOutCubic();
            LeanTween.alphaCanvas(textCG, 1, 0.5f).setEaseOutCubic();
            LeanTween.scale(textTransform.gameObject, new Vector3(1.1f, 1.1f, 1), 0.5f).setEaseInCirc().setDelay(2f);
            LeanTween.alphaCanvas(textCG, 0, 0.5f).setEaseInCubic().setDelay(2f);
            LeanTween.scale(backTransform.gameObject, new Vector3(0f, 0f, 1), 0.5f).setEaseInCirc().setDelay(2f).setOnComplete(DisableGameObj);
        });
    }

    void DisableGameObj()
    {
        this.gameObject.SetActive(false);
    }

    void OnDisable()
    {
        LeanTween.cancel(this.gameObject);
        LeanTween.cancel(backTransform.gameObject);
        LeanTween.cancel(textTransform.gameObject);
    }
}
