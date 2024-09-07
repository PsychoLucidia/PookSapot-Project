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
    public RectTransform characterNameTransform;
    public RectTransform splashArtTransform;
    public RectTransform lightningTransform;
    public RectTransform backgroundTransform;
    public RectTransform characterPanelTransform;

    [Header("Enemy Transforms")]
    public RectTransform enemyCharNameTransform;
    public RectTransform enemysplashArtTransform;

    [Header("Canvas Groups")]
    public CanvasGroup buttonHighlightCanvasGroup;

    [Header("Button GameObjects")]
    public GameObject[] characterButtons;

    [Header("Public Components")]
    public Image characterName;
    public Image splashArtImage;

    [Header("Indexes")]
    public int currentSelIndex;
    public int previousSelIndex;

    [Header("Bools")]
    [SerializeField] bool _isCharacterSelected = false;

    [Header("Lists Init")]
    [SerializeField] List<RectTransform> buttonsTransforms = new List<RectTransform>();

    [Header("Positions Init")]
    [SerializeField] Vector2 charNamePositionInit;
    [SerializeField] Vector2 charSplashPositionInit;
    [SerializeField] Vector2 enemyNamePositionInit;
    [SerializeField] Vector2 enemySplashPositionInit;
    [SerializeField] Vector2 lightningPositionInit;
    [SerializeField] List<Vector2> buttonPositions = new List<Vector2>();
    [SerializeField] List<CanvasGroup> buttonsCanvasGroup = new List<CanvasGroup>();

    [Header("Coroutines")]
    Coroutine characterBtnsCoroutine;
    Coroutine characterSelectedCoroutine;

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
        GameManager.instance.gameState = GameState.CharSelect;

        EnterCharacterSelect();
        Fader.instance.gameObject.SetActive(true);
        Fader.instance.faderCG.alpha = 1;
        Fader.instance.FadeEnable(0, 1, false, 0);
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            OnChangeIndex(currentSelIndex - 1);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            OnChangeIndex(currentSelIndex + 1);
        }
        */
    }

    /// <summary>
    /// Called when the user changes the selected index.
    /// </summary>
    /// <param name="newIndex">The new index that the user selected.</param>
    public void OnChangeIndex(int newIndex)
    {
        // If the new index is different from the current index, update the selected index and animate the character name and splash art
        if (newIndex != currentSelIndex && !_isCharacterSelected)
        {
            // Store the previous selected index
            previousSelIndex = currentSelIndex;

            // Update the current selected index
            currentSelIndex = newIndex;

            // Update the character name and splash art based on the new selected index
            SetCharacterNameAndSplashArt();

            // Animate the character name and splash art
            AnimateCharacterNameAndSplashArt();

            // Move the button highlight to the new selected index
            MoveButtonHighlight();
        }
    }

    public void SelectCharacter()
    {
        if (!_isCharacterSelected)
        {
            GameManager.instance.playerInfo = fighterInfo[currentSelIndex];

            int enemyRandomizer = Random.Range(0, fighterInfo.Length);
            GameManager.instance.enemyInfo = fighterInfo[enemyRandomizer];

            _isCharacterSelected = true;

            characterSelectedCoroutine = StartCoroutine(OnCharacterSelected());
        }
    }

    /// <summary>
    /// Sets the character name and splash art sprites based on the current selected index.
    /// </summary>
    void SetCharacterNameAndSplashArt()
    {
        // Set the character name sprite based on the current selected index
        characterName.sprite = fighterInfo[currentSelIndex].characterName;

        // Set the splash art sprite based on the current selected index
        splashArtImage.sprite = fighterInfo[currentSelIndex].characterSplashArt;
    }

    /// <summary>
    /// Animate the character name and splash art when the character index changes.
    /// </summary>
    void AnimateCharacterNameAndSplashArt()
    {
        // Move the splash art position to the left and scale the character name to 0
        splashArtTransform.anchoredPosition = charSplashPositionInit - new Vector2(1000, 0);
        characterNameTransform.anchoredPosition = charNamePositionInit - new Vector2(20, 30);
        characterNameTransform.localScale = Vector3.zero;

        // Cancel any previous animations
        LeanTween.cancel(splashArtTransform.gameObject);
        LeanTween.cancel(buttonHighlightTransform.gameObject);
        LeanTween.cancel(characterNameTransform.gameObject);

        // Animate the splash art moving to the right and character name scaling to normal size
        LeanTween.move(splashArtTransform, charSplashPositionInit - new Vector2(20, 0), 0.1f)
        .setOnComplete(() => LeanTween.move(splashArtTransform, charSplashPositionInit, 1f).setEaseOutCirc());

        LeanTween.move(characterNameTransform, charNamePositionInit, 1.1f).setEaseOutCirc();
        LeanTween.scale(characterNameTransform.gameObject, new Vector3(1.1f, 1.1f, 1f), 0.05f)
        .setEaseOutCirc()
        .setOnComplete(() =>
        {
            // Scale the character name down to 0.9f scale
            LeanTween.scale(characterNameTransform.gameObject, new Vector3(0.9f, 0.9f, 1f), 0.05f)
            .setEaseInCirc()
            .setOnComplete(() =>
            {
                // Scale the character name up to normal size
                LeanTween.scale(characterNameTransform.gameObject, Vector3.one, 1f).setEaseOutCirc();
            });
        });
    }

    void AnimateEnemyCharacterNameSplashArt()
    {

    }

    /// <summary>
    /// Move the button highlight to the selected index.
    /// </summary>
    void MoveButtonHighlight()
    {
        // Move the button highlight to the selected index
        buttonHighlightTransform.localPosition = buttonsTransforms[currentSelIndex].localPosition;
        buttonHighlightCanvasGroup.alpha = 0;

        // Scale the button highlight to 1.1f and then scale it back to normal with a ease out circ animation
        buttonHighlightTransform.localScale = new Vector3(1.1f, 1.1f, 1f);
        LeanTween.scale(buttonHighlightTransform.gameObject, Vector3.one, 0.2f).setEaseOutCirc();
        LeanTween.alphaCanvas(buttonHighlightCanvasGroup, 1f, 0.2f).setEaseOutCirc();
    }

    /// <summary>
    /// This function is called once when the script is initalized.
    /// It saves the initial position of the character name and splash art,
    /// and also saves all the transforms of the character buttons in the list buttonsTransforms.
    /// </summary>
    void Initalization()
    {
        // Save the initial position of the character name
        charNamePositionInit = characterNameTransform.anchoredPosition;

        // Save the initial position of the splash art
        charSplashPositionInit = splashArtTransform.anchoredPosition;

        // Save the initial position of the enemy character name
        enemyNamePositionInit = enemyCharNameTransform.anchoredPosition;

        // Save the initial position of the enemy splash art
        enemySplashPositionInit = splashArtTransform.anchoredPosition;  

        // Save the initial position of the lightning
        lightningPositionInit = lightningTransform.anchoredPosition;

        foreach (GameObject obj in characterButtons)
        {
            CanvasGroup cgComponent = obj.GetComponent<CanvasGroup>();

            buttonsCanvasGroup.Add(cgComponent);
        }

        // Add all the transform of the buttons to the list buttonsTransforms
        foreach (GameObject obj in characterButtons)
        {
            // Get the RectTransform of the button
            RectTransform rectTransform = obj.GetComponent<RectTransform>();

            // Add the RectTransform to the list
            buttonsTransforms.Add(rectTransform);
        }

        foreach (RectTransform obj in buttonsTransforms)
        {
            // Set the initial position of the button
            buttonPositions.Add(obj.anchoredPosition);
        }


    }

    void EnterCharacterSelect()
    {
        lightningTransform.anchoredPosition = new Vector2(lightningPositionInit.x, lightningPositionInit.y + 1400f);
        backgroundTransform.localScale = new Vector3(1.1f, 1.1f, 1f);
        characterPanelTransform.localScale = new Vector3(0f, 0f, 1f);

        for (int i = 1; i < buttonPositions.Capacity; i++)
        {
            buttonsTransforms[i - 1].anchoredPosition = new Vector2(buttonPositions[i - 1].x, buttonPositions[i - 1].y - 20f);
            buttonsCanvasGroup[i - 1].alpha = 0f;
            buttonsCanvasGroup[i - 1].interactable = false;
        }

        LeanTween.scale(backgroundTransform.gameObject, Vector3.one, 0.5f).setEaseOutCirc()
        .setOnComplete(() =>
        {
            LeanTween.move(lightningTransform, new Vector2(lightningPositionInit.x, lightningPositionInit.y - 20f), 0.5f).setEaseInCubic()
            .setOnComplete(() => 
            {
                LeanTween.move(lightningTransform, lightningPositionInit, 1f).setEaseOutCirc();
                LeanTween.scale(characterPanelTransform.gameObject, new Vector3(1.1f, 1.1f, 1f), 0.15f).setEaseOutCubic()
                .setOnComplete(() =>
                {
                    LeanTween.scale(characterPanelTransform.gameObject, Vector3.one, 0.15f).setEaseInCubic()
                    .setOnComplete(() =>
                    {
                        characterBtnsCoroutine = StartCoroutine(AnimateCharacterButtons());
                    });
                });
            });
        });
    }

    IEnumerator AnimateCharacterButtons()
    {
        for (int i = 1; i < buttonPositions.Capacity; i++)
        {
            LeanTween.move(buttonsTransforms[i - 1], new Vector2(buttonPositions[i - 1].x, buttonPositions[i - 1].y), 0.5f).setEaseOutBounce();
            LeanTween.alphaCanvas(buttonsCanvasGroup[i - 1], 1f, 0.5f).setEaseOutCubic();
            yield return new WaitForSeconds(0.1f);
        }

        SetCharacterNameAndSplashArt();
        UiManager.instance.gameObjects[0].SetActive(true);
        UiManager.instance.gameObjects[1].SetActive(true);
        UiManager.instance.gameObjects[2].SetActive(true);

        for (int i = 1; i < buttonPositions.Capacity; i++)
        {
            buttonsCanvasGroup[i - 1].interactable = true;
        }

        AnimateCharacterNameAndSplashArt();
        MoveButtonHighlight();
        yield break;
    }

    IEnumerator OnCharacterSelected()
    {
        LeanTween.cancel(buttonHighlightTransform.gameObject);

        LeanTween.scale(buttonHighlightTransform.gameObject, new Vector3(3f, 3f, 1f), 0.5f).setEaseInCubic();
        LeanTween.scale(buttonsTransforms[currentSelIndex], new Vector3(1.2f, 1.2f, 1f), 0.2f).setEaseOutCirc();
        LeanTween.alphaCanvas(buttonHighlightCanvasGroup, 0f, 0.5f).setEaseInCubic();

        yield return new WaitForSeconds(1f);

        Fader.instance.gameObject.SetActive(true);
        Fader.instance.FadeEnable(1, 0.5f, true, 3);
        yield break;
    }
}
