using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class MultiplayerCharSelect : MonoBehaviourPunCallbacks
{
    [Header("Assets")]
    public FighterInfo[] fighterInfo;

    [Header("Parent Object")]
    [SerializeField] GameObject _characterParentObj;

    [Header("Transforms for Player 1 and Player 2")]
    public RectTransform buttonHighlightTransform_Player1;
    public RectTransform buttonHighlightTransform_Player2;

    [Header("Canvas Groups")]
    public CanvasGroup buttonHighlightCanvasGroup_Player1;
    public CanvasGroup buttonHighlightCanvasGroup_Player2;

    [Header("UI Elements - Player 1")]
    public Image characterName_Player1;
    public Image splashArtImage_Player1;

    [Header("UI Elements - Player 2 (Opponent)")]
    public Image characterName_Player2;
    public Image splashArtImage_Player2;

    [Header("Button GameObjects")]
    public GameObject[] characterButtons;

    [Header("Indexes")]
    public int currentSelIndex;
    public int previousSelIndex;
    private int opponentSelIndex = -1;

    [Header("Player States")]
    public bool playerReady = false;
    public bool opponentReady = false;

    [SerializeField] bool _isCharacterSelected = false;

    void Start()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    void Update()
    {
        // Only handle input for the local player.
        if (photonView.IsMine && !_isCharacterSelected)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                OnChangeIndex(currentSelIndex - 1);
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                OnChangeIndex(currentSelIndex + 1);
            }

            if (Input.GetKeyDown(KeyCode.Return))
            {
                SelectCharacter();
            }
        }
    }

    /// <summary>
    /// Called when the user changes the selected index.
    /// </summary>
    public void OnChangeIndex(int newIndex)
    {
        if (newIndex != currentSelIndex && !_isCharacterSelected)
        {
            previousSelIndex = currentSelIndex;
            currentSelIndex = newIndex;

            SetLocalPlayerCharacterNameAndSplashArt();
            MoveButtonHighlight_Player1(); // Move the highlight for the local player.

            // Send the new selection to the other player.
            photonView.RPC("SyncCharacterSelection", RpcTarget.Others, currentSelIndex);
        }
    }

    public void SelectCharacter()
    {
        if (!_isCharacterSelected)
        {
            playerReady = true;

            // Assign the local player’s selected character.
            GameManager.instance.playerInfo = fighterInfo[currentSelIndex];

            // Notify the opponent that this player is ready.
            photonView.RPC("OpponentReadyState", RpcTarget.Others, true);

            CheckIfBothPlayersReady();
            _isCharacterSelected = true;
        }
    }

    [PunRPC]
    public void SyncCharacterSelection(int selectedIndex)
    {
        opponentSelIndex = selectedIndex;
        SetOpponentCharacterNameAndSplashArt();
        MoveButtonHighlight_Player2(); // Move the opponent’s highlight.
    }

    [PunRPC]
    public void OpponentReadyState(bool ready)
    {
        opponentReady = ready;
        CheckIfBothPlayersReady();
    }

    void CheckIfBothPlayersReady()
    {
        if (playerReady && opponentReady)
        {
            StartCoroutine(LoadBattleScene());
        }
    }

    IEnumerator LoadBattleScene()
    {
        yield return new WaitForSeconds(1f);
        PhotonNetwork.LoadLevel("BattleScene");
    }

    /// <summary>
    /// Sets the local player's character name and splash art.
    /// </summary>
    void SetLocalPlayerCharacterNameAndSplashArt()
    {
        characterName_Player1.sprite = fighterInfo[currentSelIndex].characterName;
        splashArtImage_Player1.sprite = fighterInfo[currentSelIndex].characterSplashArt;
    }

    /// <summary>
    /// Sets the opponent's character name and splash art based on the synced index.
    /// </summary>
    void SetOpponentCharacterNameAndSplashArt()
    {
        if (opponentSelIndex >= 0 && opponentSelIndex < fighterInfo.Length)
        {
            characterName_Player2.sprite = fighterInfo[opponentSelIndex].characterName;
            splashArtImage_Player2.sprite = fighterInfo[opponentSelIndex].characterSplashArt;
        }
    }

    void MoveButtonHighlight_Player1()
    {
        buttonHighlightTransform_Player1.localPosition = characterButtons[currentSelIndex].transform.localPosition;
        buttonHighlightCanvasGroup_Player1.alpha = 1;
    }

    void MoveButtonHighlight_Player2()
    {
        buttonHighlightTransform_Player2.localPosition = characterButtons[opponentSelIndex].transform.localPosition;
        buttonHighlightCanvasGroup_Player2.alpha = 1;
    }
}
