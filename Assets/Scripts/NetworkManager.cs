using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

[System.Serializable]
public class DefaultRoom
{
    public string Name;
    public int sceneIndex;
    public int maxPlayer;
}

public class NetworkManager : MonoBehaviourPunCallbacks
{
    public List<DefaultRoom> defaultRooms;
    public GameObject roomUIPanel;
    public GameObject roomUI;
    

    public void ConnectToServer()
    {
        PhotonNetwork.ConnectUsingSettings();
        Debug.Log("Try Connect To Server...");
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Server...");
        base.OnConnectedToMaster();
        PhotonNetwork.JoinLobby();     
    }

    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        Debug.Log("WE JOINED THE LOBBY");
        roomUIPanel.SetActive(true);
        roomUI.SetActive(true);
    }

    public void InitiliazeRoom(int defaultRoomIndex)
    {
        DefaultRoom roomSetting = defaultRooms[defaultRoomIndex];

        //LOAD SCENE
        PhotonNetwork.LoadLevel(roomSetting.sceneIndex);
        
        
        //CREATE THE ROOM
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = (byte)roomSetting.maxPlayer;
        roomOptions.IsVisible = true;
        roomOptions.IsOpen = true;

        PhotonNetwork.JoinOrCreateRoom(roomSetting.Name, roomOptions, TypedLobby.Default);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Joined a Room");
        base.OnJoinedRoom();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log("A new player joined the room");
        base.OnPlayerEnteredRoom(newPlayer);
    }

}
