using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterPanel : MonoBehaviour
{
    [Header("Assets")]
    public FighterInfo[] fighterInfo;

    [Header("Parent Object")]
    [SerializeField] GameObject characterParentObj;

    [Header("Transforms")]
    public RectTransform buttonHighlightTransform;

    [Header("Button GameObjects")]
    public GameObject[] characterButtons;

    [Header("Public Components")]
    public Image characterName;
    public Image splashArtImage;
    public RectTransform characterNamePos;
    public RectTransform splashArtPos;

    [Header("Indexes")]
    public int currentSelIndex;
    public int previousSelIndex;

    [Header("Lists Init")]
    [SerializeField] List<RectTransform> buttonsTransforms = new List<RectTransform>();

    [Header("Init")]
    [SerializeField] Vector2 charNamePositionInit;
    [SerializeField] Vector2 charSplashPositionInit;

    void Awake()
    {
        characterButtons = new GameObject[characterParentObj.transform.childCount];

        for (int i = 0; i < characterParentObj.transform.childCount; i++)
        {
            characterButtons[i] = characterParentObj.transform.GetChild(i).gameObject;
        }

        Initalization();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            OnChangeIndex(currentSelIndex - 1);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            OnChangeIndex(currentSelIndex + 1);
        }
    }

    public void OnChangeIndex(int index)
    {
        if (index != currentSelIndex)
        {
            previousSelIndex = currentSelIndex;
            currentSelIndex = index;

            characterName.sprite = fighterInfo[currentSelIndex].characterName;
            splashArtImage.sprite = fighterInfo[currentSelIndex].characterSplashArt;

            splashArtPos.anchoredPosition = new Vector2(charSplashPositionInit.x - 1000, charSplashPositionInit.y);
            characterNamePos.anchoredPosition = new Vector2(charNamePositionInit.x -20, charNamePositionInit.y - 30);
            characterNamePos.localScale = new Vector3(0f, 0f, 1);

            LeanTween.cancel(splashArtPos.gameObject);
            LeanTween.cancel(buttonHighlightTransform.gameObject);
            LeanTween.cancel(characterNamePos.gameObject);
            
            LeanTween.move(splashArtPos, new Vector2(charSplashPositionInit.x - 20, charSplashPositionInit.y), 0.1f).setOnComplete(() => {
                LeanTween.move(splashArtPos, charSplashPositionInit, 1f).setEaseOutCirc();
            });

            buttonHighlightTransform.localPosition = buttonsTransforms[currentSelIndex].localPosition;

            buttonHighlightTransform.localScale = new Vector3(1.1f, 1.1f, 1f);
            LeanTween.scale(buttonHighlightTransform.gameObject, new Vector3(1f, 1f, 1f), 0.2f).setEaseOutCirc();
            // LeanTween.moveLocal(buttonHighlightTransform.gameObject, buttonsTransforms[currentSelIndex].localPosition, 0.1f).setEaseInOutBounce();

            LeanTween.move(characterNamePos, new Vector2(charNamePositionInit.x, charNamePositionInit.y), 1.1f).setEaseOutCirc();
            LeanTween.scale(characterNamePos.gameObject, new Vector3(1.1f, 1.1f, 1f), 0.05f).setEaseOutCirc().setOnComplete(() => {
                LeanTween.scale(characterNamePos.gameObject, new Vector3(0.9f, 0.9f, 1f), 0.05f).setEaseInCirc().setOnComplete(() => {
                    LeanTween.scale(characterNamePos.gameObject, new Vector3(1f, 1f, 1f), 1f).setEaseOutCirc();
                });
            });
        }
    }

    void Initalization()
    {
        charNamePositionInit = characterNamePos.anchoredPosition;
        charSplashPositionInit = splashArtPos.anchoredPosition;

        // Add all the transform of the buttons to the list buttonsTransforms
        foreach (GameObject obj in characterButtons)
        {
            RectTransform rectTransform = obj.GetComponent<RectTransform>();

            buttonsTransforms.Add(rectTransform);
        }
    }
}
