using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class PlayerItem : MonoBehaviour
{
    public TextMeshProUGUI playerName;
    public Image playerImage;

    public Color player1Color;
    public Color player2Color;
    
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("hello");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetPlayerInfo(Player _player)
    {
        playerName.text = "Player";
    }

    public void ApplyLocalChanges()
    {
        playerImage.color = player1Color;
    }
}
