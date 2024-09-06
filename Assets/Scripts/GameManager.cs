using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameState gameState; // references the GameState script to determine the state of the game scene
    public PauseState pauseState;   // references the PauseState script to determine the status if the game is paused, unpaused, or is in slow motion

    public FighterInfo playerInfo;
    public FighterInfo enemyInfo;

    // Start is called before the first frame update
    void Awake()
    {   
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);    //  if a game manager has been duplicated, destroys the duplicate
        }
    }

    // Update is called once per frame
    void Update()
    {
        PauseStates();
    }
    
    public void PauseStates()
    {
        switch (pauseState) // determines if the game is in pause state
        {
            case PauseState.Paused: Time.timeScale = 0; break;  // the game is paused if the value is set to 0
            case PauseState.Unpaused: Time.timeScale = 1; break;    // the game continues once the value is set to 1
            
            default: break; 
        }
    }
}
