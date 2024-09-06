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

    /// <summary>
    /// Initialize the transforms and canvas groups and play the intro tweens.
    /// </summary>
    void Start()
    {
        // Initialize the transforms and canvas groups
        RootLogoAlphaTransformInit(new Vector3(1, 1, 1), 1f);
        PookTransformInit(new Vector2(0, -250), new Vector3(1, 0, 1));
        LabananTransformInit(new Vector2(-300, -1500), Quaternion.Euler(0, 0, -90));
        BarInit(0f);
        PressButtonInit(new Vector2(-30, -440), 0f);

        // Play the intro tweens
        PookIntroTween(new Vector3(1, 1, 1), new Vector2(0, -95), 1f);
        LabananIntroTween(new Vector3(1, 1, 1), new Vector2(-0, -155), Quaternion.Euler(0, 0, 0), 0.5f);
    }

    /// <summary>
    /// Initializes the root logo's transform with the specified size and alpha value.
    /// </summary>
    /// <param name="size">The size to set for the logo transform.</param>
    /// <param name="alpha">The alpha value to set for the logo color.</param>
    public void RootLogoAlphaTransformInit(Vector3 size, float alpha)
    {
        // Set the local scale of the logo transform to the specified size.
        logoTransform.localScale = size;

        // Set the alpha value of the logo canvas group to the specified alpha.
        logoCG.alpha = alpha;
    }

    /// <summary>
    /// Initializes the Pook Sapot transform by setting its local position and scale.
    /// </summary>
    /// <param name="pos">The new local position of the transform.</param>
    /// <param name="size">The new local scale of the transform.</param>
    public void PookTransformInit(Vector2 pos, Vector3 size)
    {
        // Sets the local position of the Pook Sapot transform to the specified position.
        pookSapotTransform.localPosition = pos;

        // Sets the local scale of the Pook Sapot transform to the specified size.
        pookSapotTransform.localScale = size;
    }

    /// <summary>
    /// Initializes the Labanan transform with the specified position and rotation.
    /// </summary>
    /// <param name="pos">The position to set for the Labanan transform.</param>
    /// <param name="rotate">The rotation to set for the Labanan transform.</param>
    public void LabananTransformInit(Vector2 pos, Quaternion rotate)
    {
        // Sets the local position of the Labanan transform to the specified position.
        labananTransform.localPosition = pos;

        // Sets the local rotation of the Labanan transform to the specified rotation.
        labananTransform.localRotation = rotate;
    }


    /// <summary>
    /// Initializes the bar image with the specified fill amount.
    /// </summary>
    /// <param name="imageFill">The fill amount to set for the bar image.</param>
    public void BarInit(float imageFill)
    {
        // Sets the fill amount of the bar image to the specified value.
        barImage.fillAmount = imageFill;
    }

    /// <summary>
    /// Initializes the press button text transform with the specified position and alpha value.
    /// </summary>
    /// <param name="pos">The position to set for the press button transform.</param>
    /// <param name="alpha">The alpha value to set for the press button color.</param>
    public void PressButtonInit(Vector2 pos, float alpha)
    {
        // Sets the local position of the press button transform to the specified position.
        pressBtnTransform.localPosition = pos;

        // Sets the alpha value of the press button color to the specified value.
        pressBtnCG.alpha = alpha;
    }

    public void RootLogoAlphaTransformTween(Vector3 size, float alpha, float time)
    {
        LeanTween.scale(logoTransform.gameObject, size, time).setEaseInCirc();
    }

    /// <summary>
    /// Tweens the Pook Sapot transform to introduce it on the screen.
    /// </summary>
    /// <param name="size">The original size of the Pook Sapot transform.</param>
    /// <param name="pos">The target position of the Pook Sapot transform.</param>
    /// <param name="time">The total time for the tweening effect.</param>
    public void PookIntroTween(Vector3 size, Vector2 pos, float time)
    {
        // Scale the Pook Sapot transform's Y-axis to 1.5 times its original size over a duration of 0.75 times the specified time
        // with a circular ease-out effect, then scale it back to its original size over the remaining time with a bounce effect. 
        LeanTween.scaleY(pookSapotTransform.gameObject, size.y * 1.5f, time * 0.75f).setEaseOutCirc().setDelay(1f).setOnComplete(() => 
        {
            // Scale the Pook Sapot transform's Y-axis back to its original size with a bounce effect. 
            LeanTween.scaleY(pookSapotTransform.gameObject, size.y, time).setEaseOutBounce();
        });

        // Move the Pook Sapot transform's local Y-position to 1.1 times the specified position over the specified time.
        LeanTween.moveLocalY(pookSapotTransform.gameObject, pos.y, time).setEaseOutCirc().setDelay(1f);
    }

    /// <summary>
    /// Tweens the Labanan transform to introduce it on the screen.
    /// </summary>
    /// <param name="size">The original size of the Labanan transform.</param>
    /// <param name="pos">The target position of the Labanan transform.</param>
    /// <param name="rotate">The target rotation of the Labanan transform.</param>
    /// <param name="time">The total time for the tweening effect.</param>
    public void LabananIntroTween(Vector3 size, Vector2 pos, Quaternion rotate, float time)
    {
        // Scale the Labanan transform's X-axis to 1.5 times its original size over a duration of 0.75 times the specified time
        // with a circular ease-out effect, then scale it to 0.7 times its original size over 0.5 times the specified time with a circular ease-in effect,
        // and finally scale it back to its original size over the remaining 0.5 times the specified time with a bounce effect.
        LeanTween.scaleX(labananTransform.gameObject, size.x * 1.5f, time * 0.75f).setEaseOutCirc().setDelay(1.5f).setOnComplete(() =>
        {
            LeanTween.scaleX(labananTransform.gameObject, size.x * 0.7f, time * 0.5f).setEaseInCirc().setOnComplete(() =>
            {
                LeanTween.scaleX(labananTransform.gameObject, size.x, time * 0.5f).setEaseOutBounce();
                // Play the press button tween at the specified position.
                BarTween(1f, 0.5f, new Vector2(0, -440));
            });
        });

        // Rotate the Labanan transform's Z-axis to the specified rotation over the specified time with a cubic ease-out effect.
        LeanTween.rotateZ(labananTransform.gameObject, rotate.z, time).setEaseOutCubic().setDelay(1.5f);

        // Move the Labanan transform's local X and Y positions to the specified positions over the specified time with a circular ease-out effect.
        LeanTween.moveLocalX(labananTransform.gameObject, pos.x, time).setEaseOutCirc().setDelay(1.5f);
        LeanTween.moveLocalY(labananTransform.gameObject, pos.y, time).setEaseOutCirc().setDelay(1.5f);
    }

    
    /// <summary>
    /// Tweens the bar image's fill amount to the specified value over the specified time, 
    /// then triggers the press button tween at the specified position.
    /// </summary>
    /// <param name="imageFill">The target fill amount of the bar image.</param>
    /// <param name="time">The time for the tweening effect.</param>
    /// <param name="pos">The position for the press button tween.</param>
    public void BarTween(float imageFill, float time, Vector2 pos)
    {
        // Tween the bar image's fill amount to the specified value over the specified time with a cubic ease-in effect.
        // When the tween is complete, trigger the press button tween at the specified position.
        LeanTween.value(barImage.fillAmount, imageFill, time).setEaseInCubic().setOnUpdate((float val) => { barImage.fillAmount = val; }).setOnComplete(() => 
        {
            // Trigger the press button tween at the specified position with an alpha value of 1 over a duration of 0.5 seconds.
            PressButtonTween(pos, 1f, 0.5f);
        });
    }

    
    /// <summary>
    /// Tweens the press button text to the specified position with the specified alpha value over the specified time.
    /// </summary>
    /// <param name="pos">The target position of the press button.</param>
    /// <param name="alpha">The target alpha value of the press button.</param>
    /// <param name="time">The time for the tweening effect.</param>
    public void PressButtonTween(Vector2 pos, float alpha, float time)
    {
        // Set the press button's alpha value to the specified value.
        pressBtnCG.alpha = alpha;

        // Tween the press button's local X position to the specified value over the specified time with a circular ease-out effect.
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

    /// <summary>
    /// Cancels all tweens on the TitleTween object.
    /// </summary>
    public void CancelTween()
    {
        // Cancel the tweens on the logo, pook sapot, labanan, bar image, and press button transforms.
        LeanTween.cancel(logoTransform.gameObject);
        LeanTween.cancel(pookSapotTransform.gameObject);
        LeanTween.cancel(labananTransform.gameObject);
        LeanTween.cancel(barImage.gameObject);
        LeanTween.cancel(pressBtnTransform.gameObject);
    }
}
