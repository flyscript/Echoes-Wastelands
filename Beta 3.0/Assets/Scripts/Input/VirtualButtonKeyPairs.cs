/*
  Keymap Manager
  
  Will Chapman
  21/07/2018
  
  Describes a pairing of a real key toa  virtual key
         
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirtualButtonKeyPairs : MonoBehaviour
{
	/// <summary>
	/// The virtual button that the real key is paired to
	/// </summary>
	[SerializeField] [Tooltip("The Virtual Button GameObject", order = 1)]
	private GameObject _virtualButton;
	
	/// <summary>
	/// The real key that the virtual button is paired to
	/// </summary>
	[SerializeField] [Tooltip("The Virtual Button GameObject", order = 2)]
	private KeyCode[] _realKeys;
	
	
	
	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	public GameObject VirtualButton
	{
		get { return _virtualButton; }
		set { _virtualButton = value; }
	}
	
	public KeyCode[] RealKeys
	{
		get { return _realKeys; }
		set { _realKeys = value; }
	}
}
