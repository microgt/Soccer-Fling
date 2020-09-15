using UnityEngine;
using UnityEngine.UI;

// attached to each room on join list to do a join if the players hit the button
public class MatchJoiner : MonoBehaviour {

	public Text btnText;

	private string RoomName;

	public void Join () {
		PhotonNetwork.JoinRoom(this.RoomName);
	}

	public void UpdateRoom(RoomInfo room) {
		this.name = RoomName;
		btnText.text = this.RoomName;
	}

}