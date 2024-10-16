using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ServerManager : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();

        base.OnConnectedToMaster();
    }

    public override void OnJoinedLobby()
    {

        Debug.Log("Joined Lobby");
        base.OnJoinedLobby();
    }

    public override void OnJoinedRoom()
    {

        Debug.Log("Joined Room");
        PhotonNetwork.LoadLevel("CharacterSelect_Multiplayer");
        base.OnJoinedRoom();
    }

    public override void OnCreatedRoom()
    {

        Debug.Log("Room Created");
        base.OnCreatedRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("Failed to Join");
        base.OnJoinRandomFailed(returnCode, message);
    }
}
