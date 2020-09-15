using UnityEngine;
using System.Collections;

public class NetworkSyncScript : Photon.MonoBehaviour {
	//private Vector3 correctPlayerVel = Vector3.zero;
	private Vector3 correctPlayerVel = Vector3.zero;
	private Vector3 correctPlayerPos = Vector3.zero;//We lerp towards this
	private Quaternion correctPlayerRot = Quaternion.identity; //We lerp towards this
	private Vector3 angularVelocity = Vector3.zero;
	private Vector3 rbPosition = Vector3.zero;
	Rigidbody rb;
	void Start() {
		rb = GetComponent <Rigidbody> ();
	}
	void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		if (stream.isWriting)
		{
			//We own this player: send the others our data
			//    stream.SendNext((int)controllerScript._characterState);
//			stream.SendNext(transform.position);
			stream.SendNext(transform.rotation);
//			stream.SendNext(rb.velocity);
			stream.SendNext (rb.position);

		}
		else
		{
			//Network player, receive data
			//    controllerScript._characterState = (CharacterState)(int)stream.ReceiveNext();
//			correctPlayerPos = (Vector3)stream.ReceiveNext();
			correctPlayerRot = (Quaternion)stream.ReceiveNext();
//			correctPlayerVel = (Vector3)stream.ReceiveNext();
//			angularVelocity = (Vector3)stream.ReceiveNext();
			rbPosition = (Vector3)stream.ReceiveNext ();

		}
	}


	void Update()
	{
		if (!photonView.isMine)
		{
			//Update remote player (smooth this, this looks good, at the cost of some accuracy)
			//rb.isKinematic = true;
//			rb.velocity = Vector3.Lerp(rb.velocity, correctPlayerVel, Time.deltaTime * 5);
//			transform.position = Vector3.Lerp(transform.position, correctPlayerPos, Time.deltaTime * 5);
			transform.rotation = Quaternion.Lerp(transform.rotation, correctPlayerRot, Time.deltaTime * 5);
//			rb.angularVelocity = Vector3.Lerp(rb.angularVelocity, angularVelocity, 0.1f);
			rb.position = Vector3.Lerp (rb.position, rbPosition, 0.1f);

		}
	}
}
