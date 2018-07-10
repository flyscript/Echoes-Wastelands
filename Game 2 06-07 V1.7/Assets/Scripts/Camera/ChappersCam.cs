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
	Snap
}

public class ChappersCam : MonoBehaviour
{

	//Class Variables
	[SerializeField]   private     bool          chappersCam;
	                   private     System.Random rand                  = new System.Random();
	[SerializeField]   private     CameraType    camType;
	
	//Camera Position variables
	[SerializeField]   private     GameObject    cameraTarget;
	[SerializeField]   private     Vector3       currentTarget;
	[SerializeField]   private     Vector3       targetOffset          = new Vector3(0f, 0f, -10f);
	[SerializeField]   private     bool          shakeCam;
	[SerializeField]   private     Vector3       shakeFactor;
	
	//Distances character can move from the centre before tracking to them
	[SerializeField]   private     float         leftBound             = 5f;
	[SerializeField]   private     float         rightBound            = 5f;
	[SerializeField]   private     float         upperBound            = 5f;
	[SerializeField]   private     float         lowerBound            = 5f;
	
	
	[SerializeField]
	[Range(1f, 128f)]
	private     float         smoothFactor    = 16f;
	
	
	void Start()
	{
		//Initialise ChappersCam, then set RunCamera to true
		cameraTarget = GameObject.Find("Character");
		currentTarget = cameraTarget.transform.position;
		camType = CameraType.Track;
	}

	//Won't use this camera trickery when not required.
	public bool RunCamera
	{
		get { return chappersCam;  }
		set { chappersCam = value;  }
	}

	public GameObject CameraTarget
	{
		get { return cameraTarget; }
		set { cameraTarget = value; }
	}

	//Call with a float intensity of how much camera hake to add
	public void Shake(float _intensity)
	{
		shakeCam = true;
		shakeFactor = new Vector3(rand.Next(10), rand.Next(10), 0).normalized * _intensity;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (this.chappersCam)
		{
			if (shakeCam)
			{
				//TODO: Future development could include smooth springing along a defined axis
				shakeFactor *= -0.9f;
				if (shakeFactor.magnitude < 0.1f)
				{
					shakeFactor = new Vector3(0, 0, 0);
					shakeCam = false;
				}
			}
			
			switch (camType)
			{
				case CameraType.Push:
					break;
				
				case CameraType.Snap:
					break;
				
				case CameraType.Track:
					//Set current target to itself, plus the difference bewteen it and the donut's position(+ an offset), eased by targetTween
					currentTarget += (cameraTarget.transform.position+targetOffset - currentTarget ) / smoothFactor;
					break;
				
				default:
					Debug.LogWarning("Camera type was not valid");
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
			transform.Translate(currentTarget - transform.position);	
			transform.LookAt(currentTarget);
		}
	}
	
}