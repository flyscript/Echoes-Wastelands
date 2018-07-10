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
	[SerializeField]
	private float _maxSpeed;
	[SerializeField]
	private bool _facingRight = true;
	private Animator _anim;
	[SerializeField]
	private bool _grounded = false;
	[SerializeField]
	private Transform _groundCheck;
	[SerializeField]
	private float _groundRadius = 0.2f;
	[SerializeField]
	private LayerMask _whatIsGround;
	private Rigidbody2D _rigidBody;
	[SerializeField]
	private float _jumpForce = 1000;
	public bool CanDoubleJump;
	private float _move;
	private KeyMapManager _input;
	private PlayerMain _playerMain;
	
	
	// Use this for initialization
	private void Start ()
	{
		_anim = GetComponent<Animator>();
		_rigidBody = GetComponent<Rigidbody2D>();
		_input = GameObject.Find("InputSystem").GetComponent<KeyMapManager>();
		_playerMain = gameObject.GetComponent<PlayerMain>();
	}

	// Update is called once per frame
	private void FixedUpdate ()
	{
		if (!_playerMain.Dead)
		{
			_grounded = Physics2D.OverlapCircle(_groundCheck.position, _groundRadius, _whatIsGround);
			_anim.SetBool("Ground", _grounded);
			_anim.SetFloat("vSpeed", _rigidBody.velocity.y);
	
			_anim.SetFloat("Speed", Mathf.Abs(_move));
			_rigidBody.velocity = new Vector2(_move * _maxSpeed / 2, _rigidBody.velocity.y);
	
			if((_move > 0 && !_facingRight) || (_move < 0 && _facingRight)) Flip();
	
			//sprinting 
			//TODO: Callum, you could change this to use a _sprint bool, that's set to true or false on key events like for Move() in KeyMapManager - Will
			if(_input.GetVirtual("Sprint").Position[0] == 1f)
			{
				_rigidBody.velocity = new Vector2(_move * _maxSpeed , _rigidBody.velocity.y);
			}
		}

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
	/// Sets direction for horizontal movement
	/// </summary>
	public float Move
	{
		get { return _move; }
		set { _move = value; }
	}
}