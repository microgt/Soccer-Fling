using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Remoting.Channels;

public class BallScript : MonoBehaviour {

	public float sensitivity = 0.5f;
	private Vector3 correctPlayerVel = Vector3.zero;
	private Vector3 correctPlayerPos = Vector3.zero;//We lerp towards this
	private Quaternion correctPlayerRot = Quaternion.identity; //We lerp towards this
	private Vector3 angularVelocity = Vector3.zero;
	Vector3 startPos;
	Vector3 endPos;
	Vector3 direction;
	PhotonView m_PhotonView;
	Rigidbody rb;

	void Start ()
	{
//		PhotonNetwork.sendRate = 20;
//		PhotonNetwork.sendRateOnSerialize = 20;
		m_PhotonView = GetComponent <PhotonView> ();
		rb = GetComponent <Rigidbody> ();
	}





	void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		if (stream.isWriting)
		{
			//We own this player: send the others our data

			stream.SendNext(transform.position);
			stream.SendNext(transform.rotation);
			stream.SendNext(rb.velocity);
			stream.SendNext (rb.angularVelocity);

		}
		else
		{
			//Network player, receive data

			correctPlayerPos = (Vector3)stream.ReceiveNext();
			correctPlayerRot = (Quaternion)stream.ReceiveNext();
			correctPlayerVel = (Vector3)stream.ReceiveNext();
			angularVelocity = (Vector3)stream.ReceiveNext();


		}
	}












	void Update ()
	{
		if (!m_PhotonView.isMine)
		{
				//Update remote player (smooth this, this looks good, at the cost of some accuracy)

				rb.velocity = Vector3.Lerp(rb.velocity, correctPlayerVel, Time.deltaTime * 5);
				transform.position = Vector3.Lerp(transform.position, correctPlayerPos, Time.deltaTime * 5);
				transform.rotation = Quaternion.Lerp(transform.rotation, correctPlayerRot, Time.deltaTime * 5);
				rb.angularVelocity = Vector3.Lerp(rb.angularVelocity, angularVelocity, 0.1f);

		}

		else{
			if(Input.GetMouseButtonDown (0))
			{
				startPos = new Vector3 (Input.mousePosition.x, 0, Input.mousePosition.y);
			}
			if(Input.GetMouseButtonUp (0))
			{
				endPos = new Vector3 (Input.mousePosition.x, 0, Input.mousePosition.y);
				FlingBall (endPos - startPos, Vector3.Distance (startPos, endPos) * sensitivity);
			}
		}

	}
		

	public void FlingBall (Vector3 _direction ,float _power)
	{
		//rb.AddF1orce (_direction * _power, ForceMode.Force);
		rb.velocity = _direction;
		//m_PhotonView.RPC("MoveBall", PhotonTargets.AllBuffered, _direction); 
	}



//	[PunRPC]
//	public void MoveBall(Vector3 _direction)
//	{
//		rb.velocity = _direction/10;
//	}
//





}
