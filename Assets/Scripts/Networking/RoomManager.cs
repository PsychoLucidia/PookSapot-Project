using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public TMP_InputField roomCodeInput;

    #region buttonOnClick

    public void CreateRoom()
    {
        PhotonNetwork.CreateRoom(roomCodeInput.text);
    }

    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(roomCodeInput.text);
    }

    #endregion
}
