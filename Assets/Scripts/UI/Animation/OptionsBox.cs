using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsBox : MonoBehaviour
{
    [Header("Tweening")]
    public Transform boxTransform;
    public Transform rootButtonsTransform;
    public Transform webBackgroundTransform;
    public CanvasGroup backBGCG;

    [Header("Button GameObjects")]
    public GameObject[] btnGameObjects;

    [Header("Options GameObjects")]
    public GameObject[] optionsObjs;

    [Header("Settings")]
    [SerializeField] int optionsIndex;
    [SerializeField] int creditsIndex;
    [SerializeField] int previousIndex;

    [Header("Lists")]
    public List<Vector3> buttonPositions = new List<Vector3>();
    public List<Vector3> optionsPositions = new List<Vector3>();
    public List<Transform> buttonsTransforms = new List<Transform>();
    public List<Transform> optionsButtons = new List<Transform>();
    public List<Button> buttons = new List<Button>();

    // Start is called before the first frame update
    void Awake()
    {
        Initalization();
    }

    void OnEnable()
    {
        previousIndex = 0;
        ChangeIndex(0);
        OptionsAnimations(1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeIndex(int index)
    {
        Debug.Log("Changed Index: " + index);
        previousIndex = optionsIndex;
        optionsIndex = index;

        OnChangeIndex();
        OpenSection();
    }

    void OnChangeIndex()
    {
        buttonsTransforms[previousIndex].localPosition = 
            new Vector2(buttonPositions[previousIndex].x, buttonPositions[previousIndex].y);
        buttons[previousIndex].interactable = true;


        buttonsTransforms[optionsIndex].localPosition = 
            new Vector2(buttonPositions[optionsIndex].x, buttonPositions[optionsIndex].y + 20f);
        buttons[optionsIndex].interactable = false;
    }

    void OpenSection()
    {
        optionsObjs[previousIndex].SetActive(false);
        optionsObjs[optionsIndex].SetActive(true);
    }

    public void OptionsAnimations(int index)
    {
        switch (index)
        {
            case 1:
                CancelTween();

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
                CancelTween();

                boxTransform.localScale = boxTransform.localScale;
                boxTransform.rotation = boxTransform.rotation;
                webBackgroundTransform.localScale = webBackgroundTransform.localScale;
                backBGCG.alpha = backBGCG.alpha;
                
                LeanTween.scale(boxTransform.gameObject, new Vector3(0, 0, 1), 0.2f).setEaseInCirc();
                LeanTween.rotateZ(boxTransform.gameObject, -50, 0.2f).setEaseInCirc();
                LeanTween.scale(webBackgroundTransform.gameObject, new Vector3(2, 2, 1), 0.2f).setEaseInCirc();
                LeanTween.alphaCanvas(backBGCG, 0, 0.2f).setEaseInCubic().setOnComplete(() => { this.gameObject.SetActive(false); });

                break;
            default:
                break;
        }
    }

    void CancelTween()
    {
        LeanTween.cancel(boxTransform.gameObject);
        LeanTween.cancel(webBackgroundTransform.gameObject);
        LeanTween.cancel(backBGCG.gameObject);
    }

    void Initalization()
    {
        foreach (GameObject obj in btnGameObjects)
        {
            buttonPositions.Add(obj.transform.localPosition);
        }

        foreach (GameObject obj in btnGameObjects)
        {
            buttonsTransforms.Add(obj.transform);
        }

        foreach (GameObject obj in btnGameObjects)
        {
            Button buttonComponent = obj.GetComponent<Button>();

            if (buttonComponent != null)
            {
                buttons.Add(buttonComponent);
            }
        }

        foreach (GameObject obj in optionsObjs)
        {
            optionsPositions.Add(obj.transform.localPosition);
        }

        foreach (GameObject obj in optionsObjs)
        {
            optionsButtons.Add(obj.transform);
        }

        // Disable all other options section except the first one
        int index = 0;
        foreach (GameObject obj in optionsObjs)
        {
            if (index != 0) { obj.SetActive(false); Debug.Log("Disabled Index: " + index); }

            index++;
        }
    }
}