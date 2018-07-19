/*
  Falling Block
  
  Logan Lai
  Will Chapman
  17/07/2018
  
  This describes a rock that will fall, killing the player on contact.
  
  But, after it has hit the ground, the player can move it and it will NOT kill the player
  
*/

//Using
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using JetBrains.Annotations;
using UnityEngine;

/// <summary>
/// Rolling rock behaviour
/// </summary>
public class RollingRock : Triggerable
{
	/// <summary>
	/// Event overriding base Triggerable method is fired by the trigger
	/// </summary>
	/// <param name="player"></param>
	public override void TriggerEvent(GameObject player)
	{
		Debug.Log("Falling block activated");
		if (gameObject.GetComponent<Rigidbody2D>() != null)
		{
			Debug.Log("player valid and rigid body exists");
			//Sets player for OnTriggerEnter2D
			Player = player;
	    
			//Makes rock fall
			gameObject.GetComponent<Rigidbody2D>().simulated = true;
			gameObject.GetComponent<Collider2D>().isTrigger = true;
		}
	}
	
	/// <summary>
	/// Collision event
	/// </summary>
	/// <param name="other">Object collided with</param>
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (Player != null && other != Player && !other.isTrigger && 
		    Time.fixedTime-TimeActivated > 0.25)
		{
			//If the player's been defined and that's not what we hit, and it's not a trigger, then it must be the ground.
			Debug.Log("Rock Hit Ground");
			//Makes it unable to trigger OnEnter2D
			gameObject.GetComponent<Collider2D>().isTrigger = false;
			gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
			//Camera.main.GetComponent<ChappersCam>().Shake(1f, 0.7f);
		}
	}
	
}