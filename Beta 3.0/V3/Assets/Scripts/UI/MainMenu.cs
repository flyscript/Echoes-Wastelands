using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
	/*
	  Main Menu
	  
	  Logan Lai
	  09/07/2018
	  
	  CODE REFERENCES
	  
	  This is the Main Menu Code for adding functionality to the Main Menu UI. Here, the variables take the UI Elements
	  and creates a reference for them. Below is various functions that either sets them visible or not
	*/

	[SerializeField]
	private Transform mainMenu;
	[SerializeField]
	private Transform optionsMenu;
	[SerializeField]
	private Transform inventoryMenu;

	private int currentScene;
	private int nextScene;

	private void Start()
	{
		Restart();
		mainMenu.gameObject.SetActive(true);

		currentScene = SceneManager.GetActiveScene().buildIndex;
		nextScene = currentScene + 1;
	}

	public void NewGame()
	{
		SceneManager.LoadScene(nextScene);
	}

	public void Options()
	{
		if (optionsMenu.gameObject.activeInHierarchy == false)
		{
			Restart();
			optionsMenu.gameObject.SetActive(true);
		}
	}

	public void InventoryMenu()
	{
		if (inventoryMenu.gameObject.activeInHierarchy == false)
		{
			Restart();
			inventoryMenu.gameObject.SetActive(true);
		}
	}
	
	public void BackToMain()
	{
		if (mainMenu.gameObject.activeInHierarchy == false)
		{
			Restart();
			mainMenu.gameObject.SetActive(true);
		}
	}

	public void ExitGame()
	{
		Application.Quit();
		//EditorApplication.isPlaying = false;
	}

	private void Restart()
	{
		mainMenu.gameObject.SetActive(false);
		optionsMenu.gameObject.SetActive(false);
		inventoryMenu.gameObject.SetActive((false));
	}

	public void SetFullScreen(bool isFullScreen)
	{
		//Screen.fullScreen = isFullScreen;
		Screen.SetResolution(Screen.width, Screen.height, isFullScreen);
	}
	
}
