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

using System;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEditor.Experimental.UIElements.GraphView;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

public class PlayerMain : MonoBehaviour
{
	/*//////////////////////////////
	///   Initialise Variables   ///
	//////////////////////////////*/

	//Player variables
	[SerializeField]
	private float _health;
	
	[SerializeField]
	private bool _immortal;
	
	[SerializeField]
	private float _immortalityTime;
	
	private float _timeStartedImmortal;
	
	[SerializeField]
	private bool _dead;
	
	[SerializeField]
	private List<Item> _inventory;
	
	[SerializeField]
	private int _points;
	
	[SerializeField]
	[Range(1, 5)]
	private float _sprintMultiplier;

	[SerializeField]
	[Range(0, 20)]
	float _deathShakeIntensity;
	
	[SerializeField]
	[Range(0, 1)]
	float _deathShakeLength;
	

	//References
	private CharacterControllerScript _ccs;

	private string _dataFile;
	
	
	/*//////////////////////////////
	///      Unity Methods       ///
	//////////////////////////////*/

	/// <summary>
	/// Initialises Player
	/// </summary>
	void Start ()
	{
		//Define player variables
		_health = 100f;
		_immortalityTime = 5f;
		_dead = false;
		_inventory = new List<Item>();
	
		//Define references
		_ccs = GetComponent<CharacterControllerScript>();
		_dataFile = "Assets/Scripts/Player/PlayerData.txt";

		//Run Camera
		Camera.main.GetComponent<ChappersCam>().RunCamera = true;
		
		//Print deaths
		StreamReader reader = new StreamReader(_dataFile);
		Debug.Log("Deaths: " + reader.ReadToEnd());
		reader.Close();
	}

	/// <summary>
	/// Runs every frame. Handles input for the player character here.
	/// </summary>
	void Update ()
	{
		//Only bother if not dead.
		if (!_dead)
		{
			
			//Left.
			if (Input.GetKey(KeyCode.A) ||  Input.GetKey(KeyCode.LeftArrow))
			{
				_ccs.Move = -1f;
			}

			//Right.
			if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
			{
				_ccs.Move = 1f;
			}

			//Sprint.
			//TODO: Consider adding a grounded check, feels strange being able to begin a sprint mid jump. Design.
			if (Input.GetKey(KeyCode.LeftShift))
			{
				_ccs.Move *= _sprintMultiplier;
			}

			//Jump
			if (Input.GetKey(KeyCode.Space))
			{
				_ccs.Jump = true;
			}
			else
			{
				_ccs.Jumping = false;
			}

			//Melee
			if (Input.GetKey(KeyCode.F))
			{
				_ccs.Melee = true;
			}
			
			//Immortality
			if (_immortal && Time.time - _timeStartedImmortal > 5)
			{
				_immortal = false;
				_health = 100f;
			}
		}
	}



	/*//////////////////////////////
	///          Methods         ///
	//////////////////////////////*/
    
	/// <summary>
	/// The player's hit detection mechanics
	/// </summary>
	/// <param name="other">Object the character touched</param>
	private void OnTriggerEnter2D(Collider2D other)
	{
		Debug.Log("Hit isTrigger");
		if (other.GetComponent<Trigger>() != null && other.GetComponent<Trigger>().Active)
		{
			Debug.Log("Hit Trigger");
			other.GetComponent<Trigger>().Fire(gameObject);
		}
		else if (other.GetComponent<Spike>() != null || other.GetComponent<FallingBlock>() != null  || 
		         other.GetComponent<RollingRock>() != null || other.GetComponent<AppearingSpike>() != null)
		{
			Debug.Log("Hit Block");
			Damage(500);
		}
		else if (other.GetComponent<Item>() != null)
		{
			Debug.Log("Hit Item");
			AddToInventory(other.GetComponent<Item>());
			other.GetComponent<Item>().OnPickup(gameObject);
		}
	}
	
	/// <summary>
	/// The player's current points
	/// </summary>
	public int Points
	{
		get { return _points; }
		set { _points = value; }
	}
	
	/// <summary>
	/// Damages the player by the given amount
	/// </summary>
	/// <param name="damage">Amount of health to subtract from the player</param>
	public void Damage(float damage)
	{
		if (!_dead)
		{
			//TODO: takeDamage animation to be run
		
			_health = Mathf.Max(0, _health - damage);
		
			if (_health <= 0)
			{
				Die();
			}
		}
	}
	
	/// <summary>
	/// Adds health to the player
	/// </summary>
	/// <param name="health">Amount of health to add to player</param>
	public void Heal(float health)
	{
		if (!_dead)
		{
			//TODO: healing animation?
		
			_health = Mathf.Min(100, _health + health);
		}
	}

	/// <summary>
	/// Returns the player's current health
	/// </summary>
	/// <returns>Current health</returns>
	public float GetHealth()
	{
		return _health;
	}


	/// <summary>
	/// Only to be called within Player class, runs nessecary procedure to kill player
	/// </summary>
	private void Die()
	{
		_dead = true;
		Debug.Log("PLAYER DIED");
		GetComponent<Rigidbody2D>().simulated = false;
		
		Camera.main.GetComponent<ChappersCam>().RunCamera = true;

		//TODO: death animation / functionality here
		StreamReader reader = new StreamReader(_dataFile); 
		int deaths = Int32.Parse(reader.ReadToEnd()) + 1;
		reader.Close();
		
		using (FileStream fs = new FileStream(_dataFile, FileMode.Open, FileAccess.Write))
		using (StreamWriter sw = new StreamWriter(fs))
		{
			sw.WriteLine(deaths);
		}
		
		Camera.main.GetComponent<ChappersCam>().Shake(_deathShakeIntensity, _deathShakeLength);
		
		StartCoroutine(LoadLevelAfterSeconds("Game 2 2.2", 0.5f));
	}

	/// <summary>
	/// Returns wether or not the player is dead. Read only.
	/// </summary>
	public bool Dead
	{
		get { return _dead; }
		set { /*readonly*/ Debug.LogWarning("Cannot set Dead. Property is readonly."); }
	}

	public void MakeImmortal()
	{
		_immortal = true;
		_timeStartedImmortal = Time.time;
		_health = Mathf.Infinity;
	}


	/// <summary>
	/// Method to add an item to inventory
	/// </summary>
	/// <param name="item">Item to add to inventory</param>
	public void AddToInventory(Item item)
	{
		_inventory.Add(item);
	}

	/// <summary>
	/// Method to remove an item from the inventory via item name
	/// </summary>
	/// <param name="itemName">Name of item to be removed from the inventory</param>
	public void RemoveFromInventory(string itemName)
	{
		foreach (var item in _inventory)
		{
			if (item.Name == itemName)
			{
				_inventory.Remove(item);
			}
		}
	}
	
	/// <summary>
	/// Method to remove an item from the inventory via item object
	/// </summary>
	/// <param name="item">Item object to be removed</param>
	public void RemoveFromInventory(Item item)
	{
		if (_inventory.Contains(item))
		{
			_inventory.Remove(item);
		}
	}
	
	/// <summary>
	/// Method to check if item is in inventory using a name
	/// </summary>
	/// <param name="itemName">Name of item to check</param>
	public bool IsInInventory(string itemName)
	{
		bool isIn = false;
			
		foreach (var item in _inventory)
		{
			if (item.Name == itemName)
			{
				isIn = true;
				break;
			}
		}

		return isIn;
	}
	
	/// <summary>
	/// Method to check if item is in inventory using the item itself
	/// </summary>
	/// <param name="item">Item object to check</param>
	/// <returns></returns>
	public bool IsInInventory(Item item)
	{
		return _inventory.Contains(item);
	}

	IEnumerator LoadLevelAfterSeconds(string sceneName, float seconds)
	{
		yield return new WaitForSeconds(seconds);
		SceneManager.LoadScene(sceneName);		
	}

}
