using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    Title,
    Menu,
    CharSelect,
    Cutscene,
    InGame,
    GameOver,
}

public enum PauseState
{
    Unpaused,
    Paused,
    SloMo
}
