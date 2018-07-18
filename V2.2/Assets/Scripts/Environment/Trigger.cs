/*
  Trigger
  
  Will Chapman
  06/07/2018
  
  Trigger has a variable (_triggerable) that points to a game object which has a component which inherits from Triggerable.
  It also has a variable (_trigger) that points to another trigger object which has a component which inherits from Trigger.
  
  When the player collides with a GameObject that contains one of these Triggers as a component, it will fire TriggerEvent()
  in wherever its _triggerable points, with the player GameObject as the first argument. It will also make wherever _trigger
  points Active.
  
  Player hits this trigger > trigger fires custom code inside another object > trigger makes other trigger active
  
  
  INSTRUCTIONS:
  
  Will only fire when active is set to true. Can be activated already, or can be activated by another event as game
  designer desires.
  
  When DeactivateOnTrigger is true, activate will be set to false as soon as it's triggered, to prevent the trigger
  firing more than it should on a feathery collision. Can, of course, be turned off.
  
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
public class Trigger : MonoBehaviour
{
	
	//Point to the object with a component that inherits from Triggerable, to be trigger
	[SerializeField]
	private GameObject _triggerable;
	[SerializeField]
	private GameObject _trigger;
	//Setup active bool
	[SerializeField]
	private bool _active;
	//Setup deactivate bool
	[SerializeField]
	private bool _deactivateOnTrigger;


	public virtual void Start()
	{
	}

	public virtual void Update()
	{
	}

	/// <summary>
	/// Sets what this trigger will fire
	/// </summary>
	public GameObject Triggerable
	{
		get { return _triggerable; }
		set { _triggerable = value; }
	}
	
	/// <summary>
	/// Sets whether ot not this trigger will fire
	/// </summary>
	public bool Active
	{
		get { return _active; }
		set { _active = value; }
	}
	
	/// <summary>
	/// Sets whether ot not this trigger will go inactive when fired once
	/// </summary>
	public bool DeactivateOnTrigger
	{
		get { return _deactivateOnTrigger; }
		set { _deactivateOnTrigger = value; }
	}

	/// <summary>
	/// This method is run by PlayerMain when the player hits an object with this code as component
	/// </summary>
	/// <param name="player">Has player object passed to it</param>
	public void Fire(GameObject player)
	{
		Debug.Log("Trigger Fired");
		if (_triggerable != null && _triggerable.GetComponent<Triggerable>() && _active)
		{
			Debug.Log("Triggerable Firing");
			if (_deactivateOnTrigger) _active = false;
			_triggerable.GetComponent<Triggerable>().TriggerEvent(player);
		}
		else if (_active)
		{
			Debug.LogWarning("Triggerable did not exist");
		}
		
		//Activate other Trigger
		if (_trigger != null && _trigger.GetComponent<Trigger>() != null)
		{
			_trigger.GetComponent<Trigger>().Active = true;
		}
	}
}