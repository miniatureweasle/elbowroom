﻿using UnityEngine;
using System.Collections;

public class AllPlayers : MonoBehaviour {

	public static AllPlayers instance;

	public Transform players;

	void Awake () {
		instance = this;
	}

	public bool IsAnyPlayerWithinRange(Vector3 target, float distance){
		for (int i = 0; i < players.childCount; i++) {
			if ((target - players.GetChild (i).transform.position).magnitude < distance)
				return true;
		}
		return false;
	}
}
