using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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

	[SerializeField] 
	private Text deathCounter;

	private void Start()
	{
		Time.timeScale = 1;
		
		Debug.Log(GameData.controller.data.Deaths.ToString());
		
		deathCounter.text = GameData.controller.data.Deaths.ToString();
		
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

	public void BackMain() { SceneManager.LoadScene(0); }

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
		if (optionsMenu.gameObject.activeInHierarchy == false)
		{
			Time.timeScale = 0;
			pauseScreen.gameObject.SetActive(false);
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
	
	public void SetFullScreen(bool isFullScreen)
	{
		//Screen.fullScreen = isFullScreen;
		Screen.SetResolution(Screen.width, Screen.height, isFullScreen);
	}
}
