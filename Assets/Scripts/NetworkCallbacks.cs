﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[BoltGlobalBehaviour]
public class NetworkCallbacks : Bolt.GlobalEventListener {
	
	public override void SceneLoadLocalDone(string map) {
		// randomize a position
		var pos = new Vector3(Random.Range(-16, 16), 0, Random.Range(-16, 16));

		// instantiate cube
		BoltNetwork.Instantiate(BoltPrefabs.Sphere, pos, Quaternion.identity);
	}
}
