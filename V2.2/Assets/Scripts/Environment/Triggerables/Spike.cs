/*
  Spike Triggerable
  
  Will Chapman
  Callum yates
  05/07/2018
  
  Spike object kills player when it hits them, unless it's embedded in the ground
  
*/

//Using
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using JetBrains.Annotations;
using UnityEngine;

/// <summary>
/// Falling spike behaviour
/// </summary>
public class Spike : Triggerable
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
	    
		    //Makes spike fall
		    gameObject.GetComponent<Rigidbody2D>().simulated = true;
	    }
    }
	
	/// <summary>
	/// Collision event
	/// </summary>
	/// <param name="other">Object collided with</param>
	private void OnTriggerEnter2D(Collider2D other)
	{
		Debug.Log("Hit " + other.name + "/ Type: " + other.GetType());
		if (_player != null && other != _player && !other.isTrigger && !other.CompareTag("Destructible") && !other.CompareTag("Destructible"))
		{
			//If the player's been defined and that's not what we hit, and it's not a trigger, then it must be the ground.
			Debug.Log("Spike Hit Ground");
			gameObject.GetComponent<Rigidbody2D>().simulated = false;
			//Makes it unable to trigger OnEnter2D
			gameObject.GetComponent<Collider2D>().isTrigger = false;
		}
	}
	
}