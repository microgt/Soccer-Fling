using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PUNNetworkingScript : Photon.PunBehaviour {

	public Transform spawnPoint;
	public string roomName = "room1";
	public string playerName = "Player";
	public Button leaveButton;
	public Text leaveButtonText;
	public Button joinButton;
	public Text menuButtonText;

	public GameObject canRef;
	public GameObject canGame;

	void Start ()
	{
		PhotonNetwork.ConnectUsingSettings ("v1.0");
		menuButtonText.text = "Connecting...";
		joinButton.enabled = false;

	}

	public override void OnJoinedLobby()
	{
		joinButton.enabled = true;
		menuButtonText.text = "Join/Start Game";
	}

	void OnJoinedRoom ()
	{
		PhotonNetwork.Instantiate (playerName, spawnPoint.position, spawnPoint.rotation, 0);
	}

	public void joinCreateRoom ()
	{
		canRef.SetActive (false);
		RoomOptions roomOptions = new RoomOptions() { IsVisible = false, MaxPlayers = 10 };
		PhotonNetwork.JoinOrCreateRoom(roomName, roomOptions, TypedLobby.Default);
		canGame.SetActive (true);
		leaveButtonText.text = "Leave Room";
	}

	public void leaveRoom ()
	{
		canGame.SetActive (false);
		PhotonNetwork.DestroyPlayerObjects (PhotonNetwork.player);
		PhotonNetwork.LeaveRoom ();
		canRef.SetActive (true);
	}


}
