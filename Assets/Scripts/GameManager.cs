using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameState gameState;
    public PauseState pauseState;

    // Start is called before the first frame update
    void Start()
    {   
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void PauseStates()
    {
        switch (pauseState)
        {
            case PauseState.Paused: Time.timeScale = 0; break;
            case PauseState.Unpaused: Time.timeScale = 1; break;
            default: break; 
        }
    }
}
