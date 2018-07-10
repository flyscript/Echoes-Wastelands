/*
  Pages Items
  
  Will Chapman
  Callum yates
  05/07/2018
  
  Pages is an class that inherits from Item, and sets itself up so that many different pages can be made
  
  This is just an example to be built heavily upon by Callum
  
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
		if (_player != null && other != _player && !other.isTrigger)
		{
			//If the player's been defined and that's not what we hit, and it's not a trigger, then it must be the ground.
			Debug.Log("Spike Hit Ground");
			gameObject.GetComponent<Rigidbody2D>().simulated = false;
		}
	}
	
}