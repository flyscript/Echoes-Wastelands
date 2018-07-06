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

using System.Collections.Generic;
using UnityEngine;

public class PlayerMain : MonoBehaviour
{
	/*//////////////////////////////
	///   Initialise Variables   ///
	//////////////////////////////*/

	//Player variables
	[SerializeField] private float _health;
	[SerializeField] private bool _dead;
	[SerializeField] private List<Item> _inventory;
	[SerializeField] private int _points;

	//References
	private KeyMapManager _inputSystem;
	private CharacterControllerScript _ccs;



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
		_dead = false;
		_inventory = new List<Item>();
	
		//Define references
		_inputSystem = GameObject.Find("InputSystem").GetComponent<KeyMapManager>();
		_ccs = GetComponent<CharacterControllerScript>();

		//Run Camera
		Camera.main.GetComponent<ChappersCam>().RunCamera = true;
	
	
		_inputSystem.AddEvent("Left", InputEventType.Down, delegate(InputData inputData)
		{
			_ccs.Move = -1f;
		});
		_inputSystem.AddEvent("Left", InputEventType.Up, delegate(InputData inputData)
		{
			if (_ccs.Move == -1f)
			{
				_ccs.Move = 0f;
			}
		});
	
		_inputSystem.AddEvent("Right", InputEventType.Down, delegate(InputData inputData)
		{
			_ccs.Move = 1f;
		});
		_inputSystem.AddEvent("Right", InputEventType.Up, delegate(InputData inputData)
		{
			if (_ccs.Move == 1f)
			{
				_ccs.Move = 0f;
			}
		});
	
		_inputSystem.AddEvent("Jump", InputEventType.Down, delegate(InputData inputData)
		{
			//jumping code
			if(_ccs.Grounded)
			{
				GetComponent<Animator>().SetBool("Ground", false);
				GetComponent<Rigidbody2D>().AddForce(new Vector2(0, _ccs.JumpForce));
				_ccs.CanDoubleJump = true;
			}
			else if (_ccs.CanDoubleJump)
			{
				_ccs.CanDoubleJump = false;
				GetComponent<Rigidbody2D>().AddForce(new Vector2(0, _ccs.JumpForce));
			}
		});

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
	/// Checks to see if the collided item can be picked up
	/// </summary>
	/// <param name="other"></param>
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.GetComponent<Item>() != null)
		{
			other.GetComponent<Item>().OnPickup(this);
		}
			
	}
	
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

		//TODO: death animation / functionality here
	}


	public void AddToInventory(Item item)
	{
		_inventory.Add(item);
	}

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

	public bool IsInInventory(Item item)
	{
		return _inventory.Contains(item);
	}


}
