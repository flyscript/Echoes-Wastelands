/*
  Player Main
  
  Callum yates
  Will Chapman
  05/07/2018
  
  Core controller script for the character, managed by PlayerMain
  
*/

//using
using UnityEngine;

/// <summary>
/// Character Controller handles player movement
/// </summary>
public class CharacterControllerScript : MonoBehaviour
{
	//Movement variables
	[SerializeField]
	[Range(0, 30)]
	private float _maxSpeed;
	
	[SerializeField]
	private float _jumpForce;
	
	private float _move;
	
	//Logic Variables
	[SerializeField]
	private bool _facingRight = true;
	
	[SerializeField]
	private bool _grounded = false;
	
	[SerializeField]
	private Transform _groundCheck;
	
	[SerializeField]
	private float _groundRadius = 0.2f;
	
	[SerializeField]
	private LayerMask _whatIsGround;
	
	public bool _canDoubleJump;
	
	public bool _jumpedSinceGrounded;
	
	private bool _melee;
	
	
	//Component Variables
	private Animator _anim;
	
	private Rigidbody2D _rigidBody;
	
	private PlayerMain _playerMain;
	
	
	// Use this for initialization
	private void Start ()
	{
		_anim = GetComponent<Animator>();
		_rigidBody = GetComponent<Rigidbody2D>();
		_playerMain = gameObject.GetComponent<PlayerMain>();
	}

	// Update is called once per frame
	private void FixedUpdate ()
	{
		if (!_playerMain.Dead)
		{
			//Do all movement.
			_grounded = Physics2D.OverlapCircle(_groundCheck.position, _groundRadius, _whatIsGround);
			_anim.SetBool("Ground", _grounded);
			_anim.SetFloat("vSpeed", _rigidBody.velocity.y);
	
			_anim.SetFloat("Speed", Mathf.Abs(_move));
			_rigidBody.velocity = new Vector2(_move * _maxSpeed, _rigidBody.velocity.y);
	
			//Should the sprite flip?
			if((_move > 0 && !_facingRight) || (_move < 0 && _facingRight)) Flip();
	
			//When done with moving, reset Move to 0. Ready for the next frame again.
			Move = 0f;
			
			//When done with moving, reset melee to false. Ready for the next frame again.
			_melee = false;
			
			//reset
			if (_grounded)
			{
				_jumpedSinceGrounded = false;
			}
		}

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
	/// whether the player has pressed melee button
	/// </summary>
	public bool Melee
	{
		get { return _melee; }
		set { _melee = value; }
	}
	
	/// <summary>
	/// Whether or not the player is on the ground
	/// </summary>
	public bool Grounded
	{
		get { return _grounded; }
		set { _grounded = value; }
	}

	/// <summary>
	/// Force used for jumps
	/// </summary>
	public float JumpForce
	{
		get { return _jumpForce; }
		set { _jumpForce = value; }
	}
	
	/// <summary>
	/// Sets direction for horizontal movement
	/// </summary>
	public float Move
	{
		get { return _move; }
		set { _move = value; }
	}
}