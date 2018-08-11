using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;
using UnityEngine.UI;

public class stars : MonoBehaviour
{
	public Text scoreDisplay;

	public int Points;
	
	public GameObject star1;
	
	public GameObject star2;
	
	public GameObject star3;
	
	
	// Use this for initialization
	void Awake ()
	{	
		star1.SetActive(false);
		star2.SetActive(false);
		star3.SetActive(false);
	}
	
	// Update is called once per frame
	void Update ()
	{
		Points = int.Parse(scoreDisplay.text);
		Debug.Log(Points);

		StarSystem(Points);
		
	}

	private void StarSystem(int starPoints)
	{
		switch (starPoints)
		{
			case 10:
				star1.SetActive(true);
				break;
			case 20:
				star2.SetActive(true);
				break;
			case 30:
				star3.SetActive(true);
				break;
		}
	}
		
}
