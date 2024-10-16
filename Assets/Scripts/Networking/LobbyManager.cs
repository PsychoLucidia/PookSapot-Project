using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using Photon.Realtime;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    public GameObject waitingForPlayers;
    public TextMeshProUGUI roomName;

    public float updateTimeInterval = 1.5f;
    float nextUpdateTime;

    public List<PlayerItem> playerLists = new List<PlayerItem> ();
    public PlayerItem playerItemPrefab;
    public Transform playerItemParent;
    void Start()
    {
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Joined Lobby");
        base.OnJoinedLobby();
    }
    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedRoom()
    {

        Debug.Log("Joined Room");
        roomName.text = "Room Name: " + PhotonNetwork.CurrentRoom.Name;
        UpdatePlayerList();
        base.OnJoinedRoom();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        UpdatePlayerList();
        base.OnPlayerEnteredRoom(newPlayer);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        UpdatePlayerList();
        base.OnPlayerLeftRoom(otherPlayer);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdatePlayerList()
    {
        foreach (PlayerItem item in playerLists)
        {
            Destroy (item.gameObject);
        }
        playerLists.Clear();

        if (PhotonNetwork.CurrentRoom == null) 
        {
            return;
        }

        foreach (KeyValuePair<int, Player> player in PhotonNetwork.CurrentRoom.Players)
        {
            PlayerItem newPlayerItem = Instantiate(playerItemPrefab, playerItemParent);
            newPlayerItem.SetPlayerInfo(player.Value);

            if (player.Value == PhotonNetwork.LocalPlayer)
            {
                newPlayerItem.ApplyLocalChanges();
            }

            playerLists.Add (newPlayerItem);
        }
    }

}
