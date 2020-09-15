using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Remoting.Channels;
using TrueSync;

public class BallScript1 : TrueSyncBehaviour {

	public override void OnSyncedInput() {
		FP accell = Input.GetAxis ("Vertical");
		FP steer = Input.GetAxis ("Horizontal");

		TrueSyncInput.SetFP (0, accell);
		TrueSyncInput.SetFP (1, steer);
	}

	public override void OnSyncedUpdate () {
		FP accell = TrueSyncInput.GetFP (0);
		FP steer = TrueSyncInput.GetFP (1);

		accell *= 10 * TrueSyncManager.DeltaTime;
		steer *= 250 * TrueSyncManager.DeltaTime;

		tsTransform.Translate (0, 0, accell, Space.Self);
		tsTransform.Rotate (0, steer, 0);
	}

}
