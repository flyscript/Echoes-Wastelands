using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
	/*
	 Pause Menu
	  
	  Logan Lai
	  09/07/2018
	  
	  CODE REFERENCES
	  
	  This is the Pause Menu Code for adding functionality to the Pause Menu UI. Here, the variables take the UI Elements
	  and creates a reference for them. Below is various functions that either sets them visible or not
	 */
	
	public Transform pauseScreen;
	public Transform optionsMenu;
	public Transform inventoryMenu;

	private void Start()
	{
		Time.timeScale = 1;
		pauseScreen.gameObject.SetActive(false);
		optionsMenu.gameObject.SetActive(false);
		inventoryMenu.gameObject.SetActive(false);
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape) && pauseScreen.gameObject.activeInHierarchy == false 
		    && optionsMenu.gameObject.activeInHierarchy == false && inventoryMenu.gameObject.activeInHierarchy == false)
		{
			Time.timeScale = 0;
			pauseScreen.gameObject.SetActive(true);
		}
	}

	public void BackMain() { SceneManager.LoadScene("MainMenu"); }

	public void Unpause()
	{
		Time.timeScale = 1;
		pauseScreen.gameObject.SetActive(false);
	}

	public void Options()
	{
		if (optionsMenu.gameObject.activeInHierarchy == false)
		{
			Time.timeScale = 0;
			pauseScreen.gameObject.SetActive(false);
			optionsMenu.gameObject.SetActive(true);
		}
	}

	public void Inventory()
	{
		if (inventoryMenu.gameObject.activeInHierarchy == false)
		{
			Time.timeScale = 0;
			pauseScreen.gameObject.SetActive(false);
			inventoryMenu.gameObject.SetActive(true);
		}
	}

	public void BackOption()
	{
		if (pauseScreen.gameObject.activeInHierarchy == false)
		{
			Time.timeScale = 0;
			optionsMenu.gameObject.SetActive(false);
			inventoryMenu.gameObject.SetActive(false);
			pauseScreen.gameObject.SetActive(true);
		}
	}
}
