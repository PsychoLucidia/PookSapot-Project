using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroTween : MonoBehaviour
{
    public CanvasGroup logoCanvasGroup;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartAnimation());
    }

    IEnumerator StartAnimation()
    {
        yield return new WaitForSecondsRealtime(3f);
        logoCanvasGroup.alpha = 0;
        LeanTween.alphaCanvas(logoCanvasGroup, 1, 0.5f).setEaseOutExpo();

        yield return new WaitForSecondsRealtime(2f);
        LeanTween.alphaCanvas(logoCanvasGroup, 0, 0.5f).setEaseInExpo();

        yield return new WaitForSecondsRealtime(0.5f);
        PookSceneManager.instance.LoadScene(1);

        yield break;
    }
}
