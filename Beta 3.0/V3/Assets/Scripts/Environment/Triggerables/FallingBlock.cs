/*
  Falling Block
  
  Will Chapman
  17/07/2018
  
  This describes a block that will fall, killing the player on contact.
  
  But, after it has hit the ground, the player CANNOT move it and it will NOT kill the player
    
*/

//Using
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Tilemaps;

/// <summary>
/// Falling block behaviour
/// </summary>
public class FallingBlock : Triggerable
{
	
	/// <summary>
	/// Event overriding base Triggerable method is fired by the trigger
	/// </summary>
	/// <param name="player"></param>
	public override void TriggerEvent(GameObject player)
	{
		Debug.Log("Falling block activated");
		//If we have a rigidbody installed and the player doesn;t exist yet (i.e: not already triggered)
		if (gameObject.GetComponent<Rigidbody2D>() != null && Player == null)
		{
			Debug.Log("player valid and rigid body exists");
			//Sets player for OnTriggerEnter2D
			Player = player;
	    
			//Makes block fall
			gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
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
			//Makes it unable to trigger OnEnter2D
			gameObject.GetComponent<Collider2D>().isTrigger = false;
			//Camera.main.GetComponent<ChappersCam>().Shake(1f, 0.7f);
			
		}
	}
	
}