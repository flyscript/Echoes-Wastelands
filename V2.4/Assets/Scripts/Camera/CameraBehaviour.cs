/*
  CameraBehaviour
  
  Will Chapman
  
  Taken from last module, needs to be refactored to work for our 2D charcater
  
*/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Describes camera behaviour
/// </summary>
public enum CameraType
{
	/// <summary>
	/// Hard tracks the player by centering them on the screen (+offset)
	/// </summary>
	Track,
	
	/// <summary>
	/// The player can push the screen around by walking into the boundaries
	/// </summary>
	Push,
	
	/// <summary>
	/// When the player hits a boundary the camera will move to center on them (+offset)
	/// </summary>
	Snap,
	
	/// <summary>
	/// Leaves the camera at a screen position
	/// </summary>
	Screen
}

public class CameraBehaviour : MonoBehaviour
{
	///////////////////////////////
	//       Class Variables     //
	///////////////////////////////
	
	/// <summary>
	/// Boolean describing Whether or not the camera will run
	/// </summary>
	[SerializeField] [Tooltip("Boolean describing whether or not the camera will run", order = 1)]
	private bool _chappersCam;
	
	/// <summary>
	/// Holds the Random object
	/// </summary>
	/// <returns>Random</returns>
	private readonly System.Random _rand = new System.Random();
	
	/// <summary>
	/// Holds the Boundary display object
	/// </summary>
	[SerializeField] [Tooltip("Holds the Boundary display object", order = 4)]
	private GameObject _boundDisplay;

	/// <summary>
	/// Whether ot not the camera is currently snapping
	/// </summary>
	private bool _snapping;
	
	/// <summary>
	/// The camera type settings describes which behaviour the camera will display
	/// </summary>
	[SerializeField] [Tooltip("The camera type settings describes which behaviour the camera will display", order = 5)]
	private CameraType camType;
	
	
	
	///////////////////////////////
	// Camera Position Variables //
	///////////////////////////////
	
	/// <summary>
	/// Points to what the camera should track
	/// </summary>
	[SerializeField] [Tooltip("Points to what the camera should track", order = 3)]
	private GameObject _cameraTarget;
	
	/// <summary>
	/// Shows the current target position
	/// </summary>
	[SerializeField] [Tooltip("Shows the current target position", order = 9)]
	private Vector3  _currentTarget;
	
	/// <summary>
	/// Sets the offset from the target
	/// </summary>
	[SerializeField] [Tooltip("Sets the offset from the target", order = 10)]
	private Vector3 _targetOffset;
	
	/// <summary>
	/// Whether or not the camera is shaking
	/// </summary>
	[SerializeField] [Tooltip("Whether or not the camera is shaking", order = 11)]
	private bool _shakeCam;
	
	/// <summary>
	/// The shake offset
	/// </summary>
	[SerializeField] [Tooltip("The shake offset", order = 12)]
	private Vector3 _shakeFactor;
	
	/// <summary>
	/// The length of a shake, from 0-1 (shake is multiplied by - Shake length every frame)
	/// </summary>
	[SerializeField] [Tooltip("The length of a shake, from 0-1 (shake is multiplied by - Shake length every frame)")]
	private float _shakeLength;
	
	/// <summary>
	/// The position of the camera for the screen behaviour
	/// </summary>
	[SerializeField] [Tooltip("The position of the camera for the screen behaviour", order = 6)]
	private Vector3 _screenPosition;
	
	/// <summary>
	/// The boundaries for push and snap behaviours (w is left, x is right, y is up, z is down)
	/// </summary>
	[SerializeField] [Tooltip("The boundaries for push and snap behaviour (w is left, x is right, y is up, z is down)", order = 7)]
	private Vector4 _boundary;
	
	/// <summary>
	/// How much the camera is smoothed when moving to track the target - should be high for good smooth snap behaviour
	/// </summary>
	[SerializeField]
	[Tooltip("How much the camera is smoothed when moving to track the target - should be high for good smooth snap behaviour", order = 8)]
	[Range(1f, 128f)]
	private float _smoothFactor = 16f;
	
	
	// Update is called once per frame
	void Update ()
	{
		if (_chappersCam)
		{
			if (_shakeCam)
			{
				//TODO: Future development could include smooth springing along a defined axis
				_shakeFactor *= -_shakeLength;
				if (_shakeFactor.magnitude < 0.01f)
				{
					_shakeFactor = new Vector3(0, 0, 0);
					_shakeCam = false;
				}
			}

			
			//Set a target to go to
			Vector3 target = new Vector3(_currentTarget.x, _currentTarget.y, 0);
			
			switch (camType)
			{
				//Character pushes against inside of bounding box to move the camera around
				case CameraType.Push:
					
					//Push left
					if (_cameraTarget.transform.position.x < _currentTarget.x + _boundary.w)
					{
						target.Set(target.x + (_cameraTarget.transform.position.x - (_currentTarget.x + _boundary.w)), target.y, target.z);
					}
					
					//Push right
					else if (_cameraTarget.transform.position.x > _currentTarget.x + _boundary.x)
					{
						target.Set(target.x + (_cameraTarget.transform.position.x - (_currentTarget.x + _boundary.x)), target.y, target.z);
					}
					
					//Push up
					if (_cameraTarget.transform.position.y > _currentTarget.y + _boundary.y)
					{
						target.Set(target.x, target.y + (_cameraTarget.transform.position.y - (_currentTarget.y + _boundary.y)), target.z);
					}
					
					//Push down
					else if (_cameraTarget.transform.position.y < _currentTarget.y + _boundary.z)
					{
						target.Set(target.x, target.y + (_cameraTarget.transform.position.y - (_currentTarget.y + _boundary.z)), target.z);
					}
					
					break;
				
				
				//Camera snaps to player (smoothing factor a key component here) when they escape a bounding box
				case CameraType.Snap:
					
					if (!_snapping &&(
						//Escaped Left
						_cameraTarget.transform.position.x < _currentTarget.x + _boundary.w ||
					    
						//Escaped Right
					    _cameraTarget.transform.position.x > _currentTarget.x + _boundary.x ||
					
						//Escaped Up
						_cameraTarget.transform.position.y > _currentTarget.y + _boundary.y ||
					    
						//Escaped Down
						_cameraTarget.transform.position.y < _currentTarget.y + _boundary.z))
					{
						Debug.Log("Outside of box!");
						_snapping = true;
					}
					else if (_snapping)
					{
						Debug.Log("Snapping " + (_currentTarget - _cameraTarget.transform.position).magnitude.ToString());
						target = _cameraTarget.transform.position;
						
						if ((_currentTarget - _targetOffset - _cameraTarget.transform.position).magnitude < 0.05f)
						{
							Debug.Log("Stopped Snapping");
							_snapping = false;
						}
					}
					break;
				
				case CameraType.Track:
					target = _cameraTarget.transform.position;
					break;
				
				case CameraType.Screen:
					_smoothFactor = 1;
					target = _screenPosition;
					break;
					
				default:
					Debug.LogError("Camera type was not valid");
					break;
			}
			
			//Set current target to itself, plus the difference bewteen it and the target's position(+ an offset), eased by smooth factor
			_currentTarget += _shakeFactor+((target + _targetOffset - _currentTarget ) / _smoothFactor);
			
			//Update position and LookAt
			transform.Translate(_currentTarget - transform.position);	
			transform.LookAt(_currentTarget);
			
			
			/*
			 //Legacy code
			 
			Get difference between donut position and origin
			Vector3 originDifference = cameraTarget.transform.position - origin;
			
			//Set desired position to the origin, plus a fraction of the difference between the origin and and donut position
			//Desired position is just a maximum offset from the origin for wherever the donut currently is
			Vector3 desiredPosition = new Vector3(
				origin.x + (originDifference.x * positionFraction.x),
				origin.y + (originDifference.y * positionFraction.y),
				origin.z + (originDifference.z * positionFraction.z) 
			) + shakeFactor;
			//Need to translate by the difference between the desired position and the current one, eased by positionSmoothFactor
			Vector3 translatePositionBy = (desiredPosition - transform.position ) / positionSmoothFactor;*/
			
		}
	}

	//Won't use this camera trickery when not required.
	public bool RunCamera
	{
		get { return _chappersCam;  }
		set { _chappersCam = value;  }
	}

	public GameObject CameraTarget
	{
		get { return _cameraTarget; }
		set { _cameraTarget = value; }
	}

	//Call with a float intensity of how much camera hake to add
	public void Shake(float intensity, float length)
	{
		_shakeCam = true;
		_shakeFactor = new Vector3(_rand.Next(100), _rand.Next(100), 0).normalized * intensity;
		_shakeLength = length;
	}
	
	
}