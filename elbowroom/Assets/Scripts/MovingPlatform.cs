﻿using UnityEngine;
using System.Collections;

public class MovingPlatform : TriggeredTrap {

	public bool moveHorizontal;
	public bool moveVertical;
	public bool flipLeft;
	public bool flipRight;
	public bool shrink;
	public float shrinkAmount;

	public int timeBetweenFlips = 2;
	private float flipInterval;
	public float timeUntilNextFlip;

	public int horizontalMoveDistance = 4;
	public int verticalMoveDistance = 4;

	public float moveSpeed = 5.0f;

	private bool moveLeft = true;

	Animator platformAnimator;

	Vector3 startingPosition;

	public GameObject platformMat;
	
	void Start () {
		startingPosition = transform.position;
		flipInterval = Time.time + timeBetweenFlips;
		platformAnimator = GetComponent<Animator> ();

	}

	public override void OnTrapActive(){

		timeUntilNextFlip = flipInterval - Time.time;
	
		if (timeUntilNextFlip > 1) {
			timeUntilNextFlip =1;
		}

		//platformMat.renderer.material.color = new Color ( 0.4f + (timeBetweenFlips - timeUntilNextFlip) , 1f ,  0.6f);

		platformMat.renderer.material.color = new Color (1 , timeUntilNextFlip ,  timeUntilNextFlip);

		bool canFlip = false;
		if (Time.time >= flipInterval) {
		
			canFlip = true;
			flipInterval = Time.time + timeBetweenFlips;
		}
		if (shrink) {
			pShrink();
		}

		if (moveHorizontal){
			moveLeft = pMoveHorizontal(moveLeft);
		}
		if (moveVertical){
			pMoveVertical();
		}
		if (canFlip & flipLeft){
			pFlipLeft();
			canFlip = false;
		}
		else if (canFlip & flipRight){
			pFlipRight();
			canFlip = false;
		}
	}

bool pMoveHorizontal(bool moveLeft){

		if (moveLeft) {
			Vector3 destination = transform.localPosition;
			destination.x = horizontalMoveDistance;
			transform.localPosition = Vector3.Slerp (transform.localPosition, destination, moveSpeed * Time.deltaTime);
			if (destination == transform.localPosition){
				return false;
			}
			return true;

		} else {
			Vector3 destination = transform.localPosition;
			destination.x = -horizontalMoveDistance;
			transform.localPosition = Vector3.Slerp (transform.localPosition, destination, moveSpeed * Time.deltaTime);
			if (destination == transform.localPosition){
				return true;
			}
			return false;
		}
	}

	void pMoveVertical(){

	}

	void pShrink(){

		if (transform.localScale.x <= 0) {
			GameObject.Destroy(this);
		}

		transform.localScale -= new Vector3(shrinkAmount,0,0);
		renderer.material.color += new Color(0,0,shrinkAmount);
	}

	void pFlipLeft(){
		platformAnimator.Play ("FlipLeft");
	}

	void pFlipRight(){
		platformAnimator.Play ("FlipRight");
	}

	// unimplemented 
	public override void OnTrapDeactivated(Collider collider){}
	public override void OnTrapActivated(Collider collider){}
}
