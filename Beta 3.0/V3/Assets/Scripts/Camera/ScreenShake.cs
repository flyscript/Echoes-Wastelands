using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour {

	private readonly System.Random _rand = new System.Random();
	
	[SerializeField] [Tooltip("Whether or not the camera is shaking", order = 11)]
	private bool _shakeCam;
	
	[SerializeField] [Tooltip("The shake offset", order = 12)]
	private Vector3 _shakeFactor;
	
	[SerializeField] [Tooltip("The length of a shake, from 0-1 (shake is multiplied by - Shake length every frame)")]
	private float _shakeLength;

	private void Update()
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
	}
	
	public void Shake(float intensity, float length)
	{
		_shakeCam = true;
		_shakeFactor = new Vector3(_rand.Next(100), _rand.Next(100), 0).normalized * intensity;
		_shakeLength = length;
	}
}
