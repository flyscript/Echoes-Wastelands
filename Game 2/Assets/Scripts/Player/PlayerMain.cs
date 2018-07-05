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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMain : MonoBehaviour
{
	private float health;
	private Dictionary<string, Item> inventory;

	// Use this for initialization
	void Start ()
	{
		health = 100f;
		inventory = new Dictionary<string, Item>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
