using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class OptionsBox : MonoBehaviour
{
    [Header("Tweening")]
    public Transform boxTransform;
    public Transform rootButtonsTransform;
    public Transform webBackgroundTransform;
    public CanvasGroup backBGCG;

    [Header("Buttons")]
    public Transform[] buttonsTransforms;

    [Header("GameObjects")]
    public GameObject[] gameObjects;

    [Header("Settings")]
    [SerializeField] int optionsIndex;
    [SerializeField] int creditsIndex;

    public List<Vector3> buttonPositions = new List<Vector3>();

    // Start is called before the first frame update
    void Awake()
    {
        foreach (GameObject obj in gameObjects)
        {
            buttonPositions.Add(obj.transform.position);
        }
    }

    void OnEnable()
    {
        ChangeIndex(0);
        OptionsAnimations(1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeIndex(int index)
    {
        optionsIndex = index;

        switch (optionsIndex)
        {
            case 0: 
                OpenSounds();
                break;
            default:
                break;
        }
    }

    void OpenSounds()
    {

    }

    void OptionsAnimations(int index)
    {
        switch (index)
        {
            case 1:
                boxTransform.localScale = new Vector3(0, 0, 1);
                boxTransform.rotation = Quaternion.Euler(0, 0, -50);
                webBackgroundTransform.localScale = new Vector3(0, 0, 1);
                backBGCG.alpha = 0;

                LeanTween.scale(boxTransform.gameObject, new Vector3(1.1f, 1.1f, 1), 0.15f).setEaseOutCubic().setOnComplete(() => {
                    LeanTween.scale(boxTransform.gameObject, new Vector3(0.9f, 0.9f, 1), 0.05f).setEaseInCubic().setOnComplete(() => {
                        LeanTween.scale(boxTransform.gameObject, new Vector3(1, 1, 1), 0.05f).setEaseOutBounce();
                    });
                });
                LeanTween.rotateZ(boxTransform.gameObject, 1, 0.15f).setEaseOutCubic().setOnComplete(() => {
                    LeanTween.rotateZ(boxTransform.gameObject, -1, 0.05f).setEaseInCubic().setOnComplete(() => {
                        LeanTween.rotateZ(boxTransform.gameObject, 0, 0.05f).setEaseOutBounce();
                    });
                });
                LeanTween.scale(webBackgroundTransform.gameObject, new Vector3(1, 1, 1), 0.2f).setEaseOutCirc();
                LeanTween.alphaCanvas(backBGCG, 1, 0.2f).setEaseOutCubic();

                break;
            case 2:
                boxTransform.localScale = boxTransform.localScale;
                boxTransform.rotation = boxTransform.rotation;
                backBGCG.alpha = backBGCG.alpha;

                break;
            default:
                break;
        }
    }
}