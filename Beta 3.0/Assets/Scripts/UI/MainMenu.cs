using System.Runtime.Serialization.Formatters;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
	/*
	  Main Menu
	  
	  Logan Lai
	  09/07/2018
	  
	  CODE REFERENCES
	  
	  This is the Main Menu Code for adding functionality to the Main Menu UI. Here, the variables take the UI Elements
	  and creates a reference for them. Below is various functions that either sets them visible or not
	  
	  *CODE WAS TAKEN FROM LAST MODULE (MED5189) AND MODIFIED*
	*/

	// References for Various UI Elements
	[SerializeField]
	private Transform mainMenu;
	[SerializeField]
	private Transform optionsMenu;
	[SerializeField]
	private Transform inventoryMenu;

	[SerializeField] 
	private Transform fileSelect;

	// METHODS
	// Runs once during the start of the Scene - Runs "Restart" method, sets UI Element "mainMenu" to active and sets
	// private variables
	private void Start()
	{
		Time.timeScale = 1;
		Restart();
		mainMenu.gameObject.SetActive(true);

	}

	// FileSelect
	public void LoadScreen()
	{
		if (fileSelect.gameObject.activeInHierarchy == false)
		{
			Restart();
			fileSelect.gameObject.SetActive(true);
		}
	}

	public void SetLevel(string level)
	{
		GameData.controller.data.Level = level;
	}

	
	public void SetSaveFile(string name)
	{
		GameData.controller.data.SaveFileName = name;
		if (!GameData.controller.Load())
		{	
			GameData.controller.Erase();
			GameData.controller.data.SaveFileName = name;
			GameData.controller.Save();
		}
	}
	
	// *METHODS BELOW DO THE SAME FUNCTION*
	// Simply takes the UI Elements and sets the appropritate one Active
	
	public void Options()
	{
		if (optionsMenu.gameObject.activeInHierarchy == false)
		{
			Restart();
			optionsMenu.gameObject.SetActive(true);
		}
	}

	public void EraseAllData()
	{
		string lastName = GameData.controller.data.SaveFileName;
		for (int i = 1; i <= 5; i++)
		{
			GameData.controller.Erase();
			GameData.controller.data.SaveFileName = "Save"+i.ToString();
			GameData.controller.Save();
		}

		GameData.controller.data.SaveFileName = lastName;
	}
	
	public void InventoryMenu()
	{
		if (inventoryMenu.gameObject.activeInHierarchy == false)
		{
			Restart();
			inventoryMenu.gameObject.SetActive(true);
			
			//TODO: populate
			//Will Chapman
			
			//Loop over every page
			for (int page = 1; page <= 5; page++)
			{
				//If page exists in save data
				if (GameData.controller.data.IsInJournal("Page " + page.ToString()))
				{
					Transform pageRef = inventoryMenu.Find("Panel").Find("Page " + page.ToString());
					
					//Set page to interactable
					pageRef.Find("Button").GetComponent<Button>().interactable = true;
					
					//Update deaths on the page
					pageRef.Find("Panel").Find("Stats").Find("DeathCount").GetComponent<Text>().text =
						GameData.controller.data.GetFromJournal("Page " + page.ToString()).LevelDeaths.ToString() + " Deaths";

					//Update gems on the page
					for (int gem = 0; gem <= 3; gem++)
					{
						if (GameData.controller.data.GetFromJournal("Page " + page.ToString()).Gems[gem])
						{
							pageRef.Find("Panel").Find("Stats").Find("Crystal"+(gem+1).ToString()).GetComponent<Image>().color = new Color(1f,1f,1f,1f);
						}
					}
					
					//Update description on the page
					pageRef.Find("Panel").Find("Narrative").Find("MainText").GetComponent<Text>().text =
						GameData.controller.data.GetFromJournal("Page " + page.ToString()).Description;

				}
				//Game is chronologically linear, so we can stop searching after the first null return
				else
				{
					break;
				}
			}
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

	// Exits the Game (in Build Only)
	public void ExitGame()
	{
		Application.Quit();
		//EditorApplication.isPlaying = false;
	}

	// Methods used to reset the UI Elements by setting them as "Not Active"
	private void Restart()
	{
		mainMenu.gameObject.SetActive(false);
		optionsMenu.gameObject.SetActive(false);
		inventoryMenu.gameObject.SetActive((false));
		fileSelect.gameObject.SetActive(false);
		//Loop over every page
		for (int page = 1; page <= 5; page++)
		{
			inventoryMenu.Find("Panel").Find("Page " + page.ToString()).Find("Button").GetComponent<Button>().interactable = false;
			for (int gem = 0; gem <= 3; gem++)
			{
				inventoryMenu.Find("Panel").Find("Page " + page.ToString()).Find("Panel").Find("Stats").Find("Crystal"+(gem+1).ToString()).GetComponent<Image>().color = new Color(0f,0f,0f,0.5f);
			}
		}
	}

	// Checks if the "Toggle" is On/Off and Sets the Full-screen boolean to the toggle
	public void SetFullScreen(bool isFullScreen)
	{
		//Screen.fullScreen = isFullScreen;
		Screen.SetResolution(Screen.width, Screen.height, isFullScreen);
	}
	
}
