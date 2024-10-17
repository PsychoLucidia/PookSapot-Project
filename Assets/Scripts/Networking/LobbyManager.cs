using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using Photon.Realtime;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    public GameObject waitingForPlayers;
    public GameObject charSelect;
    public TextMeshProUGUI roomName;
    public GameObject player1;
    public GameObject player2;

    public int playerCount; 

    public float updateTimeInterval = 1.5f;
    float nextUpdateTime;

    public PlayerItem playerItemPrefab;
    public Transform playerItemParent;
    void Start()
    {
        PhotonNetwork.JoinLobby();
        playerCount = PhotonNetwork.CurrentRoom.PlayerCount;
        Debug.Log("Joined Character Select Lobby");
        player1.gameObject.SetActive(true);
        roomName.text = "Room Name: " + PhotonNetwork.CurrentRoom.Name;
        CountPlayer();
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Joined Lobby nga");
        
        base.OnJoinedLobby();
    }
    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedRoom()
    {
        
        Debug.Log("Room name changed");
        base.OnJoinedRoom();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        CountPlayer();
        base.OnPlayerEnteredRoom(newPlayer);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        base.OnPlayerLeftRoom(otherPlayer);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CountPlayer()
    {
        playerCount = PhotonNetwork.CurrentRoom.PlayerCount;

        if (playerCount == 2)
        {
            player2.gameObject.SetActive(true);
    
        }
    }


}
