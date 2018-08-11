/*
  Level Setup
  
  Will Chapman
  09/08/2018
  
  Sets up the level as required when loaded
  
*/

//Using
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSetup : MonoBehaviour {

	// Use this for initialization
	void Start ()
	{
		//If it's ready for the purple gem
		if (GameData.controller.data.GetFromJournal(SceneManager.GetActiveScene().name).Gems[0] &&
		    GameData.controller.data.GetFromJournal(SceneManager.GetActiveScene().name).Gems[1] &&
		    GameData.controller.data.GetFromJournal(SceneManager.GetActiveScene().name).Gems[2] &&
		    !GameData.controller.data.GetFromJournal(SceneManager.GetActiveScene().name).Gems[3]
		)
		{
			GameObject.Find("Blue Gem 1").GetComponent<Gem>().Gem4.SetActive(true);
			GameObject.Find("Blue Gem 1").SetActive(false);
			GameObject.Find("Blue Gem 2").SetActive(false);
			GameObject.Find("Blue Gem 3").SetActive(false);
		}
		//Otherwise disable as necessary
		else
		{
			//First disable the purple to keep the Gem4 reference
			if (GameData.controller.data.GetFromJournal(SceneManager.GetActiveScene().name).Gems[3])
			{
				GameObject.Find("Blue Gem 1").GetComponent<Gem>().Gem4.SetActive(false);
			}
			//Then disable the others
			for (int i = 1; i <= 3; i++)
			{
				if (GameData.controller.data.GetFromJournal(SceneManager.GetActiveScene().name).Gems[i-1])
				{
					GameObject.Find("Blue Gem "+i.ToString()).SetActive(false);
				}
			}
		}
	}
}
