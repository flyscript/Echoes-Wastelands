/*
  Keymap Manager
  
  Will Chapman
  05/07/2018
  
  Sets up virtual keys and maps real ones to them
     
         
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyMapDescription : MonoBehaviour
{

	private KeyMapManager inputSystem;
	
	// Use this for initialization
	void Start ()
	{
		/*Debug.Log("Hi Cal 1");
		
		inputSystem = GameObject.Find("InputSystem").GetComponent<KeyMapManager>();
		
		//Create Virtual Keys
		inputSystem.AddVirtualButton("Jump");
		inputSystem.AddVirtualButton("Attack");
		inputSystem.AddVirtualButton("Left");
		inputSystem.AddVirtualButton("Right");
		inputSystem.AddVirtualButton("Pause");
		
		//Create keymap
		inputSystem.AddMap("Default");
		inputSystem.SetTo("Default");

		//Map real keys to virtual keys
		inputSystem.Map().AddKey("Jump", KeyCode.Space, InputType.Button);
		
		inputSystem.Map().AddKey("Attack", KeyCode.F, InputType.Button);
		
		inputSystem.Map().AddKey("Left", KeyCode.A, InputType.Button);
		inputSystem.Map().AddKey("Left", KeyCode.LeftArrow, InputType.Button);
		
		inputSystem.Map().AddKey("Right", KeyCode.D, InputType.Button);
		inputSystem.Map().AddKey("Right", KeyCode.RightArrow, InputType.Button);
		
		inputSystem.Map().AddKey("Pause", KeyCode.P, InputType.Button);
		inputSystem.Map().AddKey("Pause", KeyCode.Escape, InputType.Button);

		
		inputSystem.AddEvent("Jump", InputEventType.Down, delegate(InputData inp)
		{
			Debug.Log("Hi Cal");
		});*/

	}

	void Update()
	{
		
	}
}
