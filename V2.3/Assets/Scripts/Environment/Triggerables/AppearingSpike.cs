/*
  Appearing Spike Triggerable
  
  Will Chapman
  17/07/2018
  
  Spike object appears when triggered, kills player when it hits them
  
*/

//Using
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using JetBrains.Annotations;
using UnityEngine;

/// <summary>
/// Appearing spike behaviour
/// </summary>
public class AppearingSpike : Triggerable
{
	private GameObject _player;
	
	/// <summary>
	/// Event overriding base Triggerable method is fired by the trigger
	/// </summary>
	/// <param name="player"></param>
	public override void TriggerEvent(GameObject player)
	{
			//Sets player for OnTriggerEnter2D
			_player = player;
	    
			//Makes spike appear
			Debug.Log("spikey");
			gameObject.GetComponent<BoxCollider2D>().offset.Set(-0.85f, -2f);
			//TODO: Callum, please trigger animation here
	}
	
}