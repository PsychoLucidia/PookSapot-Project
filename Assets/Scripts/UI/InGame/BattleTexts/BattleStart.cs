using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleStart : MonoBehaviour
{
    public Transform battleStartBG;
    public Transform battleText;
    public Transform startText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnEnable() // enables the strart battle animation
    {
        battleStartBG.localScale = new Vector3(0, 0, 1);
        battleText.localScale = new Vector3(10, 10, 1);
        battleText.localPosition = new Vector3(-3, 84, 0);
        startText.localPosition = new Vector3(-2200, -7.69f, 0); 

        StartBattle();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartBattle()   // the animation for battle start
    {
        LeanTween.scale(battleText.gameObject, new Vector3(1f, 1f, 1f), 1f).setEaseInCubic();  
        LeanTween.moveLocal(startText.gameObject, new Vector3(-16.09f, 7.69f, 0f), 1f).setEaseInCubic().setOnComplete(() => {
            LeanTween.scale(battleStartBG.gameObject, new Vector3(1.3f, 1.3f, 1f), 0.1f).setEaseOutCirc().setOnComplete(() => {
                LeanTween.scale(battleStartBG.gameObject, new Vector3(1f, 1f, 1f), 0.5f).setEaseInBounce().setOnComplete(() => {
                    LeanTween.scale(battleStartBG.gameObject, new Vector3(0f, 0f, 1f), 0.1f).setEaseInCirc().setDelay(1.5f);
                    LeanTween.moveLocal(battleText.gameObject, new Vector3(-2200, -7.69f, 0f), 0.2f).setDelay(1.5f);
                    LeanTween.moveLocal(startText.gameObject, new Vector3(2200, -7.69f, 0f), 0.2f).setDelay(1.5f).setOnComplete(ChangeState);
                });
            });
        });
    }

    void ChangeState()  // handles the changes in the state of the game 
    {
        if (GameManager.instance.gameState != GameState.InGame)
        {
            GameManager.instance.gameState = GameState.InGame;
        }

        UiManager.instance.ActivateObject(0);
        this.gameObject.SetActive(false);
    }

    void OnDisable()    // cancels the animations
    {
        LeanTween.cancel(this.gameObject);
        LeanTween.cancel(battleStartBG.gameObject);
        LeanTween.cancel(battleText.gameObject);
        LeanTween.cancel(startText.gameObject);
    }
}
