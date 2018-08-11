/*
  Triggerable
  
  Will Chapman
  06/07/2018
  
  Main class for other triggerable components to inherit from
  
  
  INSTRUCTIONS:
  
  First, make a trigger. This should be a GameObject with a 2D collider, with isTrigger set to true. It should also
  contain a Trigger.cs as a component.
  
  Then, make your triggerable hazard. It should inherit from this class, and whatever code you define in TriggerEvent()
  will be run when the player touches your trigger. Use Spike.cs and the Spike GameObjects in the main Scene as a
  reference, and put your class definition in the Triggerables folder too.
  
  Once you have a triggerable hazard with its Triggerable.cs inheriting component and a trigger with its Trigger.cs
  inheriting component, drag your triggerable game object into the Triggerable property box in the Trigger.cs component
  of your trigger. This tells the trigger what to activate.
  
  
  
*/

//Using

using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using JetBrains.Annotations;
using UnityEngine;


/// <summary>
/// The class that all items should inherit from
/// </summary>
public class Triggerable : MonoBehaviour
{

	private GameObject _player;
	private float _activated;
	
	public virtual void Start()
	{
	}

	public virtual void Update()
	{
	}

	/// <summary>
	/// Base function to be overriden by specific object that inherits from this, fired by its trigger.
	/// </summary>
	/// <param name="player">Has player object passed to it</param>
	public virtual void TriggerEvent(GameObject player)
	{
		
	}
	
	/// <summary>
	/// The player object
	/// </summary>
	public GameObject Player
	{
		get { return _player; }
		set { _player = value; }
	}
	
	/// <summary>
	/// The time the trigger was activated
	/// </summary>
	public float TimeActivated
	{
		get { return _activated; }
		set { _activated = value; }
	}
}