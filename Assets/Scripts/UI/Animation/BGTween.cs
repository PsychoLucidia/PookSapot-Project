using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGTween : MonoBehaviour
{
    public Transform bgObject;
    public CanvasGroup bgCG;

    // Start is called before the first frame update
    void Start()
    {
        CancelTween();
        InitialScale(1.2f);
        Scale(1.1f, 2f, 0);
        InitialCGAlpha(0f);
        CGAlpha(1f, 2f, 3);
    }

    // Tween the scale of the Object
    public void Scale(float scaleSize, float speed, int easeType)
    {
        switch (easeType)
        {
            case 0:
                LeanTween.scale(bgObject.gameObject, new Vector3(scaleSize, scaleSize, 1), speed).setEaseInOutCubic();
                break;
            case 1:
                LeanTween.scale(bgObject.gameObject, new Vector3(scaleSize, scaleSize, 1), speed).setEaseOutCubic();
                break;
            default:
                break;
        }
    }

    public void CGAlpha(float alpha, float speed, int easeType)
    {
        switch (easeType)
        {
            case 0:
                LeanTween.alphaCanvas(bgCG, alpha, speed);
                break;
            case 1:
                LeanTween.alphaCanvas(bgCG, alpha, speed).setEaseInOutCubic();
                break;
            case 2:
                LeanTween.alphaCanvas(bgCG, alpha, speed).setEaseInCubic();
                break;
            case 3:
                LeanTween.alphaCanvas(bgCG, alpha, speed).setEaseOutCubic();
                break;
            default:
                break;            
        }
    }

    // Set the initial scale of the Object
    public void InitialScale(float scaleSize)
    {
        bgObject.localScale = new Vector3(scaleSize, scaleSize, 1);
    }

    public void InitialCGAlpha(float alpha)
    {
        bgCG.alpha = alpha;
    }

    public void CancelTween()
    {
        LeanTween.cancel(bgObject.gameObject);
    }
}
