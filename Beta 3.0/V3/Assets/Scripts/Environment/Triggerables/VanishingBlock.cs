/*
  Vanishing Block
  
  Will Chapman
  18/07/2018
  
  This describes a block that will fall, exposing the player to whatever is below
  
  Will vanish from the screen.
    
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
public class VanishingBlock : Triggerable
{
	/// <summary>
	/// Event overriding base Triggerable method is fired by the trigger
	/// </summary>
	/// <param name="player"></param>
	public override void TriggerEvent(GameObject player)
	{
		//Makes block fall
		gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
		gameObject.GetComponent<Rigidbody2D>().simulated = true;
	}
	
}