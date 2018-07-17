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

/// <summary>
/// Falling block behaviour
/// </summary>
public class FallingBlock : Triggerable
{
	private GameObject _player;
	
	/// <summary>
	/// Event overriding base Triggerable method is fired by the trigger
	/// </summary>
	/// <param name="player"></param>
	public override void TriggerEvent(GameObject player)
	{
		if (gameObject.GetComponent<Rigidbody2D>() != null)
		{
			//Sets player for OnTriggerEnter2D
			_player = player;
	    
			//Makes block fall
			gameObject.GetComponent<Rigidbody2D>().simulated = true;
		}
	}
	
	/// <summary>
	/// Collision event
	/// </summary>
	/// <param name="other">Object collided with</param>
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (_player != null && other != _player && !other.isTrigger)
		{
			//If the player's been defined and that's not what we hit, and it's not a trigger, then it must be the ground.
			Debug.Log("Spike Hit Ground");
			//Makes it unable to trigger OnEnter2D
			gameObject.GetComponent<Collider2D>().isTrigger = false;
			gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
			Debug.Log("Frozen");
			
		}
	}
	
}