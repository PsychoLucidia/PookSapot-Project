using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleTween : MonoBehaviour
{
    [Header("Transforms")]
    public Transform rootTransform;
    public Transform logoTransform;
    public Transform pookSapotTransform;
    public Transform labananTransform;
    public Transform pressBtnTransform;

    [Header("Canvas Groups")]
    public CanvasGroup rootCG;
    public CanvasGroup logoCG;
    public CanvasGroup barCG;
    public CanvasGroup pressBtnCG;

    [Header("Images")]
    public Image barImage;

    // Start is called before the first frame update. Initialize the transforms and canvas groups and play the intro tweens.
    void Start()
    {
        // Methods responsible for initializing the transforms and canvas groups
        RootLogoAlphaTransformInit(new Vector3(1, 1, 1), 1f);
        PookTransformInit(new Vector2(0, -250), new Vector3(1, 0, 1));
        LabananTransformInit(new Vector2(-300, -1500), Quaternion.Euler(0, 0, -90));
        BarInit(0f);
        PressButtonInit(new Vector2(-30, -440), 0f);

        // Methods responsible for playing the intro tweens
        PookIntroTween(new Vector3(1, 1, 1), new Vector2(0, -95), 1f);
        LabananIntroTween(new Vector3(1, 1, 1), new Vector2(-0, -155), Quaternion.Euler(0, 0, 0), 0.5f);
    }

    public void TweenTransition(int index)
    {
        switch (index)
        {
            case 1:
                
                break;
            case 2:
                break;
            case 3:
                break;
            default:
                break;
        }
    }

    // Initializes the root logo's transform with the specified size and alpha value.
    // 
    // Parameters:
    //     size (Vector3): The size to set for the logo transform.
    //     alpha (float): The alpha value to set for the logo color.
    public void RootLogoAlphaTransformInit(Vector3 size, float alpha)
    {
        logoTransform.localScale = size;
        logoCG.alpha = alpha;
    }

    // Initializes the Pook Sapot transform by setting its local position and scale.
    // 
    // Parameters:
    //   pos (Vector2): The new local position of the transform.
    //   size (Vector3): The new local scale of the transform.
    public void PookTransformInit(Vector2 pos, Vector3 size)
    {
        pookSapotTransform.localPosition = pos;
        pookSapotTransform.localScale = size;
    }

    // Initializes the Labanan transform with the specified position and rotation.
    // 
    // Parameters:
    //   pos (Vector2): The position to set for the Labanan transform.
    //   rotate (Quaternion): The rotation to set for the Labanan transform.
    public void LabananTransformInit(Vector2 pos, Quaternion rotate)
    {
        labananTransform.localPosition = pos;
        labananTransform.localRotation = rotate;
    }

    // Initializes the bar image with the specified fill amount.
    // 
    // Parameters:
    //     imageFill (float): The fill amount to set for the bar image.
    public void BarInit(float imageFill)
    {
        barImage.fillAmount = imageFill;
    }

    // Initializes the press button text transform with the specified position and alpha value.
    // 
    // Parameters:
    //     pos (Vector2): The position to set for the press button transform.
    //     alpha (float): The alpha value to set for the press button color.
    public void PressButtonInit(Vector2 pos, float alpha)
    {
        pressBtnTransform.localPosition = pos;
        pressBtnCG.alpha = alpha;
    }

    public void RootLogoAlphaTransformTween(Vector3 size, float alpha, float time)
    {
        LeanTween.scale(logoTransform.gameObject, size, time).setEaseInCirc();
    }

    // Tweens the Pook Sapot transform to introduce it on the screen.
    // 
    // This function scales the Pook Sapot transform's Y-axis to 1.5 times its original size over a duration of 0.75 times the specified time, 
    // then scales it back to its original size over the remaining time with a bounce effect. 
    // It also moves the transform's local Y-position to 1.1 times the specified position over the specified time.
    // 
    // Parameters:
    //     size (Vector3): The original size of the Pook Sapot transform.
    //     pos (Vector2): The target position of the Pook Sapot transform.
    //     time (float): The total time for the tweening effect.
    public void PookIntroTween(Vector3 size, Vector2 pos, float time)
    {
        LeanTween.scaleY(pookSapotTransform.gameObject, size.y * 1.5f, time * 0.75f).setEaseOutCirc().setDelay(1f).setOnComplete(() => 
        {
            LeanTween.scaleY(pookSapotTransform.gameObject, size.y, time).setEaseOutBounce();
        });

        LeanTween.moveLocalY(pookSapotTransform.gameObject, pos.y, time).setEaseOutCirc().setDelay(1f);
    }

    // Tweens the Labanan transform to introduce it on the screen.
    // 
    // This function scales the Labanan transform's X-axis to 1.5 times its original size over a duration of 0.75 times the specified time, 
    // then scales it to 0.7 times its original size over 0.5 times the specified time with a circular ease-in effect, 
    // and finally scales it back to its original size over the remaining 0.5 times the specified time with a bounce effect. 
    // It also rotates the transform's Z-axis to the specified rotation over the specified time with a cubic ease-out effect, 
    // and moves the transform's local X and Y positions to the specified positions over the specified time with a circular ease-out effect.
    // 
    // Parameters:
    //     size (Vector3): The original size of the Labanan transform.
    //     pos (Vector2): The target position of the Labanan transform.
    //     rotate (Quaternion): The target rotation of the Labanan transform.
    //     time (float): The total time for the tweening effect.
    public void LabananIntroTween(Vector3 size, Vector2 pos, Quaternion rotate, float time)
    {
        LeanTween.scaleX(labananTransform.gameObject, size.x * 1.5f, time * 0.75f).setEaseOutCirc().setDelay(1.5f).setOnComplete(() => 
        {
            LeanTween.scaleX(labananTransform.gameObject, size.x * 0.7f, time * 0.5f).setEaseInCirc().setOnComplete(() => 
            {
                LeanTween.scaleX(labananTransform.gameObject, size.x, time * 0.5f).setEaseOutBounce();
                BarTween(1f, 0.5f, new Vector2(0, -440));
            });
        });

        LeanTween.rotateZ(labananTransform.gameObject, rotate.z, time).setEaseOutCubic().setDelay(1.5f);

        LeanTween.moveLocalX(labananTransform.gameObject, pos.x, time).setEaseOutCirc().setDelay(1.5f);
        LeanTween.moveLocalY(labananTransform.gameObject, pos.y, time).setEaseOutCirc().setDelay(1.5f);
    }

    
    // Tweens the bar image's fill amount to the specified value over the specified time, 
    // then triggers the press button tween at the specified position.
    // 
    // Parameters:
    //     imageFill (float): The target fill amount of the bar image.
    //     time (float): The time for the tweening effect.
    //     pos (Vector2): The position for the press button tween.
    public void BarTween(float imageFill, float time, Vector2 pos)
    {
        LeanTween.value(barImage.fillAmount, imageFill, time).setEaseInCubic().setOnUpdate((float val) => { barImage.fillAmount = val; }).setOnComplete(() => 
        {
            PressButtonTween(pos, 1f, 0.5f);
        });
    }

    
    // Tweens the press button text to the specified position with the specified alpha value over the specified time.
    // 
    // Parameters:
    //     pos (Vector2): The target position of the press button.
    //     alpha (float): The target alpha value of the press button.
    //     time (float): The time for the tweening effect.
    public void PressButtonTween(Vector2 pos, float alpha, float time)
    {
        pressBtnCG.alpha = alpha;
        LeanTween.moveLocalX(pressBtnTransform.gameObject, pos.x, time).setEaseOutCirc();
    }

    public void GoMainMenu()
    {
        rootCG.alpha = 1f;
        rootTransform.localScale = new Vector3(1f, 1f, 1);
        LeanTween.scale(rootTransform.gameObject, new Vector3(1.1f, 1.1f, 1), 0.5f).setEaseInCubic().setOnComplete(() => {
            UiManager.instance.gameObjects[1].SetActive(true);
            UiManager.instance.gameObjects[2].SetActive(false);
        });
        LeanTween.alphaCanvas(rootCG, 0, 0.5f).setEaseInCubic();
    }

    // Cancels all tweens on the TitleTween object.
    public void CancelTween()
    {
        LeanTween.cancel(logoTransform.gameObject);
        LeanTween.cancel(pookSapotTransform.gameObject);
        LeanTween.cancel(labananTransform.gameObject);
        LeanTween.cancel(barImage.gameObject);
        LeanTween.cancel(pressBtnTransform.gameObject);
    }
}
