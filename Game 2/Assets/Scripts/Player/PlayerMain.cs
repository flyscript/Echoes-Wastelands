/*
  Player Main
  
  Will Chapman
  05/07/2018
  
  This is the core script for the player containing all the main functionality.
  
  Most things should go here, triggered things like combat can be described in their own scripts and fired from here
  
  Check KeymapManager for virtual keys to use, and how to connect events to them. Ask Will Chapman for directions if
  not sure.
  
  You can mess around with virtual keys if you need to.
  
*/

using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerMain : MonoBehaviour
{
	/*//////////////////////////////
	///   Initialise Variables   ///
	//////////////////////////////*/
	
	//Player variables
	private float health;
	private bool dead;
	private Dictionary<string, Item> inventory;
	
	//References
	private KeyMapManager inputSystem;
	
	
  
	/*//////////////////////////////
	///      Unity Methods       ///
	//////////////////////////////*/
	
	/// <summary>
	/// Initialises Player
	/// </summary>
	void Start ()
	{
		//Define player variables
		health = 100f;
		dead = false;
		inventory = new Dictionary<string, Item>();
		
		//Define references
		inputSystem = GameObject.Find("InputSystem").GetComponent<KeyMapManager>();
	
	}
	
	/// <summary>
	/// Runs every frame
	/// </summary>
	void Update ()
	{
		
	}
	
	
	
	/*//////////////////////////////
	///          Methods         ///
	//////////////////////////////*/

	/// <summary>
	/// Damages the player by the given amount
	/// </summary>
	/// <param name="_damage">Amount of health to subtract from the player</param>
	public void Damage(float _damage)
	{
		if (!dead)
		{
			//TODO: takeDamage animation to be run
			
			health = Mathf.Max(0, health - _damage);
			
			if (health == 0)
			{
				Die();
			}
		}
	}
	/// <summary>
	/// Adds health to the player
	/// </summary>
	/// <param name="_health">Amount of health to add to player</param>
	public void Heal(float _health)
	{
		if (!dead)
		{
			//TODO: healing animation?
			
			health = Mathf.Min(100, health + _health);
		}
	}

	/// <summary>
	/// Returns the player's current health
	/// </summary>
	/// <returns>Current health</returns>
	public float GetHealth()
	{
		return health;
	}
	

	/// <summary>
	/// Only to be called within Player class, runs nessecary procedure to kill player
	/// </summary>
	private void Die()
	{
		dead = true;

		//TODO: death animation / functionality here
	}
	
	
}
