using UnityEngine;
using TrueSync;
using System.Net;

/**
* @brief Manages player's behavior.
**/
public class PlayerBehavior1 : TrueSyncBehaviour {
	TSVector startPosition;
	TSVector endPosition;
	TSVector direction;
	bool move;
	bool log = true;
    /**
    * @brief Key to set/get player's movement from {@link TrueSyncInput}.
    **/
    private const byte INPUT_KEY_MOVE = 0;

    /**
    * @brief Key to set/get player's jump from {@link TrueSyncInput}.
    **/
    private const byte INPUT_KEY_JUMP = 1;

	private const byte INPUT_MOUSE_POS = 2;
	private const byte INPUT_MOUSE_Y = 3;

    /**
    * @brief Player's movement speed.
    **/
    public int speed;



    TSRigidBody tsRigidBody;

    /**

    /**
    * @brief Initial setup when game is started.
    **/
    public override void OnSyncedStart () {
		tsRigidBody = GetComponent<TSRigidBody>();
        // Sets sprite and animator controller based on player's id
//		if (owner.Id == 1) {
//			tsRigidBody.position = new TSVector(0, -5, 0);
//		} else {
//            TSVector offset = new TSVector(-0.63f, -5, -0.87f);
//            tsRigidBody.GetComponent<TSCollider>().Center = offset;
//			tsRigidBody.position = new TSVector(-1 - offset.x, 0, 0);
//		}

    }

    /**


    /**
    * @brief Sets player inputs.
    **/
    public override void OnSyncedInput () {
		byte jump = Input.GetButton("Fire1") ? (byte)1 : (byte)0;
		TSVector mPos = new TSVector (Input.mousePosition.x, 0, Input.mousePosition.y);
		TrueSyncInput.SetTSVector(INPUT_MOUSE_POS, mPos);
	//	TrueSyncInput.SetFP(INPUT_MOUSE_Y, mY);
        TrueSyncInput.SetByte(INPUT_KEY_JUMP, jump);
	}

    /**
    * @brief Updates player animations and movements.
    **/
    public override void OnSyncedUpdate () {

        // Set a velocity based on player's speed and inputs
		TSVector velocity = tsRigidBody.velocity;




		if (TrueSyncInput.GetByte(INPUT_KEY_JUMP) > 0 && log) {
			startPosition = TrueSyncInput.GetTSVector (INPUT_MOUSE_POS);
			move = true;
			log = false;
        }
		if (TrueSyncInput.GetByte(INPUT_KEY_JUMP) < 1  && move) {
			endPosition = TrueSyncInput.GetTSVector (INPUT_MOUSE_POS);
			direction = endPosition - startPosition;

			velocity = direction/10;
			move = false;
			log = true;
		}

        // Assigns this velocity as new player's linear velocity
		tsRigidBody.velocity = velocity;
	}
		
}