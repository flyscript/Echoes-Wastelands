/*
  Camera
  
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

public class ChappersCam : MonoBehaviour
{
	///////////////////////////////
	//       Class Variables     //
	///////////////////////////////
	
	//Boolean describing Whether or not the camera will run
	[SerializeField] [Tooltip("Boolean describing whether or not the camera will run")]
	private bool _chappersCam;
	
	//Holds the Random object
	private System.Random _rand = new System.Random();
	
	//The camera type settings describes which behaviour the camera will display
	[SerializeField] [Tooltip("The camera type settings describes which behaviour the camera will display")]
	private CameraType camType;
	
	
	///////////////////////////////
	// Camera Position Variables //
	///////////////////////////////
	
	
	//Points to what the camera should track
	[SerializeField] [Tooltip("Points to what the camera should track")]
	private GameObject _cameraTarget;
	
	//Shows the current target position
	[SerializeField] [Tooltip("Shows the current target position")]
	private Vector3  _currentTarget;
	
	//Sets the offset from the target
	[SerializeField] [Tooltip("Sets the offset from the target")]
	private Vector3 _targetOffset;
	
	//Whether or not the camera is shaking
	[SerializeField] [Tooltip("Whether or not the camera is shaking")]
	private bool _shakeCam;
	
	//The shake offset
	[SerializeField] [Tooltip("The shake offset")]
	private Vector3 _shakeFactor;
	
	//The length of a shake, from 0-1 (shake is multiplied by - Shake length every frame)
	[SerializeField] [Tooltip("The length of a shake, from 0-1 (shake is multiplied by - Shake length every frame)")]
	private float _shakeLength;
	
	//The position of the camera for the screen behaviour
	[SerializeField] [Tooltip("The position of the camera for the screen behaviour")]
	private Vector3 _screenPosition;
	
	//The boundaries for push behaviour (w is left, x is right, y is up, z is down)
	[SerializeField] [Tooltip("The boundaries for push behaviour (w is left, x is right, y is up, z is down)")]
	private Vector4 _pushBoundaries;
	
	//The boundaries for snap behaviour (w is left, x is right, y is up, z is down)
	[SerializeField] [Tooltip("The boundaries for snap behaviour (w is left, x is right, y is up, z is down)")]
	private Vector4 _snapBoundaries;
	
	//The speed the camera returns to the target in snap behaviour
	[SerializeField]
	[Tooltip("The speed the camera returns to the target in snap behaviour")]
	private float _snapSpeed;

	//How much the camera is smoothed when moving to track the target
	[SerializeField]
	[Tooltip("How much the camera is smoothed when moving to track the target")]
	[Range(1f, 128f)]
	private float _smoothFactor = 16f;
	
	
	void Start()
	{
	}
	// Update is called once per frame
	void Update ()
	{
		if (_chappersCam)
		{
			if (_shakeCam)
			{
				//TODO: Future development could include smooth springing along a defined axis
				_shakeFactor *= -_shakeLength;
				if (_shakeFactor.magnitude < 0.1f)
				{
					_shakeFactor = new Vector3(0, 0, 0);
					_shakeCam = false;
				}
			}
			
			switch (camType)
			{
				case CameraType.Push:
					break;
				
				case CameraType.Snap:
					break;
				
				case CameraType.Track:
					//Set current target to itself, plus the difference bewteen it and the target's position(+ an offset), eased by smooth factor
					_currentTarget += _shakeFactor+((_cameraTarget.transform.position+_targetOffset - _currentTarget ) / _smoothFactor);
					break;
				
				case CameraType.Screen:
					_currentTarget = _shakeFactor + _screenPosition;
					break;
					
				default:
					Debug.LogError("Camera type was not valid");
					break;
			}
			
			
			/*Get difference between donut position and origin
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
			
			//Update position and LookAt
			transform.Translate(_currentTarget - transform.position);	
			transform.LookAt(_currentTarget);
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
	public void Shake(float _intensity, float _length)
	{
		_shakeCam = true;
		_shakeFactor = new Vector3(_rand.Next(100), _rand.Next(100), 0).normalized * _intensity;
		_shakeLength = _length;
	}
	
	
}