using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public static BattleManager instance;

    public Transform playerTransform;
    public Transform enemyTransform;

    public GameObject playerObject;
    public GameObject enemyObject;

    public PlayerMovementCC playerMovementCC;
    public EnemyManager enemyManager;

    public bool isGameOver = false;


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            Debug.Log("BattleManager singleton created");
        }
        else
        {
            Destroy(gameObject);
        }

        playerObject = GameManager.instance.playerInfo.objectPrefab;
        enemyObject = GameManager.instance.enemyInfo.objectPrefabEnemy;

        playerObject.name = "Player";
        enemyObject.name = "Enemy";
    }

    // Start is called before the first frame update
    void Start()
    {
        InstantiatePlayers();
    }

    void InstantiatePlayers()
    {
        Instantiate(playerObject, playerTransform.position, playerTransform.rotation);
        playerMovementCC = GameObject.Find("Player(Clone)").transform.GetComponent<PlayerMovementCC>();

        Instantiate(enemyObject, enemyTransform.position, enemyTransform.rotation);
        enemyManager = GameObject.Find("Enemy(Clone)").transform.GetComponent<EnemyManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerMovementCC.playerState == PlayerState.Dead)
        {
            GameManager.instance.gameState = GameState.GameOver;
        }

        if (enemyManager.state == EnemyState.Dead)
        {
            GameManager.instance.gameState = GameState.GameOver;
        }
    }

    public void GameOver()
    {
        Debug.Log("GameManager.instance.gameState: " + GameManager.instance.gameState);
        Debug.Log("playerMovementCC.playerState: " + playerMovementCC.playerState);
        Debug.Log("enemyManager.state: " + enemyManager.state);

        if (!isGameOver && playerMovementCC.playerState == PlayerState.Dead && GameManager.instance.gameState == GameState.GameOver)
        {
            Debug.Log("Player Loses");
            isGameOver = true;
            StartCoroutine(PlayerLose());
        }
        else if (!isGameOver && enemyManager.state == EnemyState.Dead && GameManager.instance.gameState == GameState.GameOver)
        {
            Debug.Log("Player Wins");
            isGameOver = true;
            StartCoroutine(PlayerWin());
        }
        else
        {
            Debug.Log("Game Over - No one died");
        }
    }

    IEnumerator PlayerWin()
    {
        UiManager.instance.gameObjects[4].SetActive(true);
        UiManager.instance.gameObjects[0].SetActive(false);
        yield return new WaitForSecondsRealtime(1f);
        UiManager.instance.gameObjects[4].SetActive(false);
        UiManager.instance.enableObject[0].SetActive(true);
        yield return new WaitForSecondsRealtime(0.5f);
        UiManager.instance.gameObjects[2].SetActive(true);
        yield return new WaitForSecondsRealtime(3f);
        GameManager.instance.gameState = GameState.Menu;
        Fader.instance.gameObject.SetActive(true);
        Fader.instance.FadeEnable(1, 0.5f, true, 0);
        yield break;
    }

    IEnumerator PlayerLose()
    {
        UiManager.instance.gameObjects[5].SetActive(true);
        UiManager.instance.gameObjects[0].SetActive(false);
        yield return new WaitForSecondsRealtime(1f);
        UiManager.instance.gameObjects[5].SetActive(false);
        UiManager.instance.enableObject[0].SetActive(true);
        yield return new WaitForSecondsRealtime(0.5f);
        UiManager.instance.gameObjects[3].SetActive(true);
        yield return new WaitForSecondsRealtime(3f);
        GameManager.instance.gameState = GameState.Menu;
        UiManager.instance.enableObject[1].SetActive(true);
        Fader.instance.FadeEnable(1, 0.5f, true, 0);
        yield break;
    }
}
