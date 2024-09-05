using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FighterInfo", menuName = "ScriptableObjects/FighterInfo", order = 2)]
public class FighterInfo : ScriptableObject
{
    public Sprite characterName;
    public Sprite characterSplashArt;
    public GameObject objectPrefab;
}
