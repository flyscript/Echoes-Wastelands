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
	private float _activated;
	
	/// <summary>
	/// Event overriding base Triggerable method is fired by the trigger
	/// </summary>
	/// <param name="player"></param>
	public override void TriggerEvent(GameObject player)
	{
		Debug.Log("Falling block activated");
		//If we have a rigidbody installed and the player doesn;t exist yet (i.e: not already triggered)
		if (gameObject.GetComponent<Rigidbody2D>() != null && _player == null)
		{
			Debug.Log("player valid and rigid body exists");
			//Sets player for OnTriggerEnter2D
			_player = player;
	    
			//Makes block fall
			gameObject.GetComponent<Rigidbody2D>().simulated = true;
			gameObject.GetComponent<Collider2D>().isTrigger = true;
			_activated = Time.fixedTime;
		}
	}
	
	/// <summary>
	/// Collision event
	/// </summary>
	/// <param name="other">Object collided with</param>
	private void OnTriggerEnter2D(Collider2D other)
	{
		Debug.Log("Block Hit something");
		if (_player != null && other != _player && !other.isTrigger && 
		    Time.fixedTime-_activated > 0.25)
		{
			//If the player's been defined and that's not what we hit, and it's not a trigger, then it must be the ground.
			Debug.Log("Block Hit Ground");
			//Makes it unable to trigger OnEnter2D
			gameObject.GetComponent<Collider2D>().isTrigger = false;
			gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
			//Camera.main.GetComponent<ChappersCam>().Shake(1f, 0.7f);
			Debug.Log("Frozen");
			
		}
	}
	
}