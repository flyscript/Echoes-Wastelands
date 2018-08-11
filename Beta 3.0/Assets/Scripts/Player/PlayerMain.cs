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

//Using
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class PlayerMain : MonoBehaviour
{
	/*//////////////////////////////
	///   Initialise Variables   ///
	//////////////////////////////*/

	//Player variables
	[SerializeField]
	private float _health;
	
	[SerializeField]
	private bool _dead;
	
	
	//Movement variables
	private float _move;
	
	[SerializeField]
	[Range(0, 30)]
	private float _speed;
	
	[SerializeField]
	[Range(0, 30)]
	private float _sprintSpeed;

	[SerializeField]
	private bool _sprint;
	
	[SerializeField]
	private bool _jumping;
	
	[SerializeField]
	private float _jumpForce;
	
	[SerializeField]
	private float _massMagnitude;

	private bool _canDoubleJump;
	
	private bool _jumpedSinceGrounded;
	
	[SerializeField]
	[Range(0, 1)]
	private float _meleeRange;
	
	[SerializeField]
	[Range(0, 20)]
	float _deathShakeIntensity;
	
	[SerializeField]
	[Range(0, 1)]
	float _deathShakeLength;
	
	
	//Logic Variables
	[SerializeField]
	private bool _facingRight = true;
	
	[SerializeField]
	private bool _grounded;
	
	[SerializeField]
	private Transform _groundCheck;
	
	[SerializeField]
	private float _groundRadius = 0.2f;
	
	[SerializeField]
	private LayerMask _collidable;
	

	//Component Variables
	private Animator _anim;
	
	private Rigidbody2D _rigidBody;
	
	
	//Getting GameObject that has Particle effect
	[SerializeField] 
	private string[] _killQuote;
	
	[SerializeField] 
    private GameObject _deathParticle;

	[SerializeField] 
	private Transform _tauntPanel;

	[SerializeField] 
	private Text _deathCount;
	
	[SerializeField] 
	private Text _taunt;
    
	
	/*//////////////////////////////
	///      Unity Methods       ///
	//////////////////////////////*/

	/// <summary>
	/// Initialises Player
	/// </summary>
	void Start ()
	{
		//Define player variables
		_dead = false;
		_anim = GetComponent<Animator>();
		_rigidBody = GetComponent<Rigidbody2D>();
		
		_tauntPanel.gameObject.SetActive(false);

		_taunt.text = _killQuote[Random.Range(0, 10)];
		_deathCount.text = GameData.controller.data.Deaths.ToString() + " Deaths";
		
		//Set death particle as not Active
		_deathParticle.SetActive(false);
	
		//Save at start of level
		GameData.controller.Save();
		
		//Define input events
		
		//Jump
		KeyMapManager.manager.AddEvent("Jump", InputEventType.Down, delegate(VirtualButton inp)
		{
			if (!_dead)
			{
				if (_grounded && !_jumping)
				{
					_jumping = true;
					_canDoubleJump = true;
					_jumpedSinceGrounded = true; 
					
					GetComponent<Animator>().SetBool("Ground", false);
					GetComponent<Rigidbody2D>().AddForce(new Vector2(0, _jumpForce));
				}
				
				//Double jump.
				else if ((_canDoubleJump || !_jumpedSinceGrounded) && !_jumping)
				{
					_jumping = true;
					_canDoubleJump = false;
					_jumpedSinceGrounded = true;
					
					/*
					 Add the correct amount of force for a full jump:
					 
					 f=ma (or in this case, f=mv)
					 
					 add enough force to completely negate the current falling
					 (velocity * mass)					 
					 
					 add the leftover force
					 
					 force to add: -velocity*mass + jumpforce
					 
					 Which would work in real life, but Unity is weird, so I'll add a heuristic multiplier to mass to get the deisred effect
					 
					 RECCOMMENDED: -50
					 
					 -
					 Will
					 
					 */
					
					GetComponent<Rigidbody2D>().AddForce(new Vector2(0, (GetComponent<Rigidbody2D>().velocity.y * 
					                                                     GetComponent<Rigidbody2D>().mass*_massMagnitude) +_jumpForce));
				}
			}
		});
		
		//Left
		KeyMapManager.manager.AddEvent("Left", InputEventType.Down, delegate (VirtualButton inp) { _move = -1f; });
		KeyMapManager.manager.AddEvent("Left", InputEventType.Up, delegate (VirtualButton inp) { if (_move == -1f) {_move = 0f;} });
		
		//Right
		KeyMapManager.manager.AddEvent("Right", InputEventType.Down, delegate (VirtualButton inp) { _move = 1f; });
		KeyMapManager.manager.AddEvent("Right", InputEventType.Up, delegate (VirtualButton inp) { if (_move == 1f) {_move = 0f;} });
		
		//Sprint
		KeyMapManager.manager.AddEvent("Sprint", InputEventType.Down, delegate (VirtualButton inp) { _sprint = true; });
		KeyMapManager.manager.AddEvent("Sprint", InputEventType.Up, delegate (VirtualButton inp) { _sprint = false; });
		
		//Attack
		KeyMapManager.manager.AddEvent("Attack", InputEventType.Down, delegate(VirtualButton inp)
		{
			Vector3 direction = new Vector3(-1, 0, 0);

			if (_facingRight)
			{
				direction = direction * -1;

			}
			
			RaycastHit2D rayHit = Physics2D.Raycast(transform.position, direction, _meleeRange, _collidable, -Mathf.Infinity, Mathf.Infinity);
				
			if (rayHit.collider != null && rayHit.collider.GetComponent<Destructable>())
			{
				Debug.Log("Hit " + rayHit.collider.name);
				rayHit.collider.GetComponent<Destructable>().Destroy();
			}
			else
			{
				Debug.Log("Did not Hit");
			}
		});
		

	}

	/// <summary>
	/// Runs every frame. Handles input for the player character here.
	/// </summary>
	void FixedUpdate ()
	{
		
		//Only bother if not dead --Movement code by Callum, refactored and re-logic'd by Will
		if (!_dead)
		{
			//reset
			if (_grounded)
			{
				_jumpedSinceGrounded = false;
			}
			
			//Grounded Check
			_grounded = Physics2D.OverlapCircle(_groundCheck.position, _groundRadius, _collidable);
			_anim.SetBool("Ground", _grounded);
			_anim.SetFloat("vSpeed", _rigidBody.velocity.y);
			_anim.SetFloat("Speed", Mathf.Abs(_move));

			if (_grounded)
			{
				_jumping = false;
			}
			
			//Move
			if (_sprint)
			{
				_rigidBody.velocity = new Vector2(_move * _sprintSpeed, _rigidBody.velocity.y);
			}
			else
			{
				_rigidBody.velocity = new Vector2(_move * _speed, _rigidBody.velocity.y);
			}
	
			//Should the sprite flip?
			if((_move > 0 && !_facingRight) || (_move < 0 && _facingRight)) Flip();
	
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
		if (other.GetComponent<Trigger>() != null && other.GetComponent<Trigger>().Active)
		{
			other.GetComponent<Trigger>().Fire(gameObject);
		}
		else if (other.GetComponent<Spike>() != null || other.GetComponent<FallingBlock>() != null  || 
		         other.GetComponent<RollingRock>() != null || other.GetComponent<AppearingSpike>() != null)
		{
			Damage(500);
		}
		else if (other.GetComponent<Gem>() != null)
		{
			other.GetComponent<Gem>().OnPickup(gameObject);
		}
	}
	
	/// <summary>
	/// Damages the player by the given amount
	/// </summary>
	/// <param name="damage">Amount of health to subtract from the player</param>
	private void Damage(float damage)
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
		
		//TODO: death animation / functionality here
		
		GameData.controller.data.Deaths++;
		GameData.controller.data.GetFromJournal(SceneManager.GetActiveScene().name).LevelDeaths++;
		GameData.controller.Save();
		
		_deathParticle.SetActive(true);
		_tauntPanel.gameObject.SetActive(true);
	
		GameObject.Find("Camera").GetComponent<CameraBehaviour>().Shake(_deathShakeIntensity, _deathShakeLength);
		
		StartCoroutine(LoadLevelAfterSeconds(SceneManager.GetActiveScene().name, 1f));
	}
	
	/// <summary>
	/// Flips character around
	/// </summary>
	private void Flip()
	{
		_facingRight = !_facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
	
	/// <summary>
	/// Whether or not the player is jumping
	/// </summary>
	public bool Jumping
	{
		get { return _jumping; }
		set { _jumping = value; }
	}
	
	/// <summary>
	/// Returns wether or not the player is dead. Read only.
	/// </summary>
	public bool Dead
	{
		get { return _dead; }
		set { /*readonly*/ Debug.LogWarning("Cannot set Dead. Property is readonly."); }
	}

	
	IEnumerator LoadLevelAfterSeconds(string sceneName, float seconds)
	{
		yield return new WaitForSeconds(seconds);
		SceneManager.LoadScene(sceneName);		
	}

}
