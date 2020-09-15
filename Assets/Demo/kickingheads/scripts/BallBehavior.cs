using UnityEngine;
using TrueSync;

/**
* @brief Manages ball's behavior.
**/
public class BallBehavior : TrueSyncBehaviour {

    /**
    * @brief Controlled {@link TSRigidBody} of the ball.
    **/
	TSRigidBody tsRigidBody;

    /**
    * @brief AudioSource for sound effects.
    **/
    AudioSource audioSource;

    /**
    * @brief Initial setup.
    **/
    void Start() {
        audioSource = GetComponent<AudioSource>();
		tsRigidBody = GetComponent<TSRigidBody>();
    }

    /**
    * @brief Calls {@link #ResetProperties} when game is started.
    **/
    public override void OnSyncedStart() {
        ResetProperties();
    }

//    public override void OnSyncedUpdate() {
//		tsRigidBody.GetComponent<TSTransform>().rotation -= 5;
//    }



    /**
    * @brief It called when a goal is scored and executes the method {@link #ResetProperties}. 
    **/
    void GoalScored() {
        ResetProperties();
    }

    /**
    * @brief Places the ball in its initial position and sets to zero its linear velocity.
    **/
    public void ResetProperties() {
		tsRigidBody.position = new TSVector(0, 0, 0);
		tsRigidBody.velocity = TSVector.zero;
    }

}