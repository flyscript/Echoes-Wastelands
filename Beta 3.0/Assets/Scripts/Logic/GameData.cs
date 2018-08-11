/*
  GameData
  
  Will Chapman
  08/08/2018
  
  This script creates a singelton controller that handles data saving, loading, and inter-scene persistence.
  
  It belongs as a component of a GameData, and for the sake of debugging purposes should be in every scene as a
  root GameObject (i.e: not under anything else in the hierarchy).
  
  It can be found in prefabs under 'Level' for. When the game's actually published it really only needs to be in the
  MainMenu that the player first sees when they play the game, as it will automatically carry over to every level after
  that.
  
  In order to save or load data as well as modify it, you need only do
  
  GameData.controller.Save()
  GameData.controller.Load()
  GameData.controller.data.[VARIABLE NAME] +/-/=/whatever
    
  If you want a certain field to be included that isn't already there, simply add it in the ordinary fashion below in 
  the Data class.
  
  
  Some functionality here was learned from a unity tutorial which can be found here:
  https://unity3d.com/learn/tutorials/topics/scripting/persistence-saving-and-loading-data
  
  Basic functionality was learned from it but if you watch it or are familliar with the video you'll know that this
  script is incredibly bespoke in comparison.
  
*/

//Using
using System;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;


/// <summary>
/// Data class is serialised so that it can be put into a binary file and saved/loaded
/// This is the default data at the start of every game
/// </summary>
[Serializable]
public class Data
{
	private int _health = 100;
	private List<Page> _journal = new List<Page>();
	private int _deaths = 0;
	private string _level = "Page 1";
	private int _bgMusicPosition = 0;
	private string _saveFileName = "Save1";

	public Data()
	{
		AddToJournal(new Page("Page 1",
			"Searching for resources in the ice caves at the foot of the old temple, I've found a child. " +
			"I should protect her and bring her back to the camp. I'm worried about the strange happenings " +
			"and purple spikes eminating from within the caves.\n\n\n!I can click on the map to explore!"
		));
	}
	
	public int Health
	{
		get { return _health; }
		set { _health = value; }
	}

	public int Deaths
	{
		get { return _deaths; }
		set { _deaths = value; }
	}
	
	public string Level
	{
		get { return _level; }
		set { _level = value; }
	}
	
	public int BgMusicPosition
	{
		get { return _bgMusicPosition; }
		set { _bgMusicPosition = value; }
	}

	public string SaveFileName
	{
		get { return _saveFileName; }
		set { _saveFileName = value; }
	}

	/// <summary>
	/// Method to retrieve a page from the journal using a name
	/// </summary>
	/// <param name="pageName">Name of page to retrieve</param>
	public Page GetFromJournal(string pageName)
	{
		Page pageIn = null;

		foreach (var page in _journal)
		{
			if (page.Name == pageName)
			{
				pageIn = page;
				break;
			}
		}

		return pageIn;
	}
	
	/// <summary>
	/// Method to add a page to the journal
	/// </summary>
	/// <param name="page">Item to add to inventory</param>
	public void AddToJournal(Page page)
	{
		_journal.Add(page);
	}

	/// <summary>
	/// Method to remove a page from the journal via page name
	/// </summary>
	/// <param name="pageName">Name of page to be removed from the journal</param>
	public void RemoveFromJournal(string pageName)
	{
		foreach (var page in _journal)
		{
			if (page.Name == pageName)
			{
				_journal.Remove(page);
			}
		}
	}
	
	/// <summary>
	/// Method to remove a page from the journal via page object
	/// </summary>
	/// <param name="page">Page to be removed</param>
	public void RemoveFromIJournal(Page page)
	{
		if (_journal.Contains(page))
		{
			_journal.Remove(page);
		}
	}
	
	/// <summary>
	/// Method to check if a page is in the journal using a name
	/// </summary>
	/// <param name="pageName">Name of page to check</param>
	public bool IsInJournal(string pageName)
	{
		bool isIn = false;
			
		foreach (var item in _journal)
		{
			if (item.Name == pageName)
			{
				isIn = true;
				break;
			}
		}

		return isIn;
	}
	
	/// <summary>
	/// Method to check if a page is in the journal using the page object itself
	/// </summary>
	/// <param name="page">Page object to check</param>
	/// <returns></returns>
	public bool IsInJournal(Page page)
	{
		return _journal.Contains(page);
	}
}


/// <summary>
/// GameData class turns itself into a singleton object and centrally controls use of Data
/// </summary>
public class GameData : MonoBehaviour
{

	//Variables

	/// <summary>
	/// Reference to GameData controller object (this) for use with singleton behaviour
	/// </summary>
	public static GameData controller;

	/// <summary>
	/// Reference to the GameData object for the sake of eliminating data replication
	/// </summary>
	private Data _data;




	//Unity methods

	/// <summary>
	/// Awake utilised by Unity instantiates our singleton behaviour
	/// </summary>
	void Awake()
	{
		//if we're the first GameData controller
		if (controller == null)
		{
			DontDestroyOnLoad(gameObject);
			controller = this;

			//if Data doesn't already exist - shouldn't need a check but just incase the reference was eliminated via fuckery
			if (_data == null)
			{
				Erase();
			}

			//Load up Save1's data if it exists, if not then create Save1
			if (!Load())
			{
				Save();
			}

		}
		//if singleton already exists and isn't this
		else if (controller != this)
		{
			Destroy(gameObject);
		}
	}




	//Custom methods

	/// <summary>
	/// Readonly reference to data object
	/// </summary>
	public Data data
	{
		get { return _data; }
	}

	public void Erase()
	{
		_data = new Data();
	}

	
	/// <summary>
	/// Saves game data
	/// </summary>
	public void Save()
	{
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create(Application.persistentDataPath + "/" + _data.SaveFileName + ".frt");
		
		bf.Serialize(file, _data);
		file.Close();
		
		Debug.Log("Data saved to " + Application.persistentDataPath + "/" + _data.SaveFileName + ".frt");
	}

	/// <summary>
	/// Loads game data
	/// </summary>
	public bool Load()
	{
		//Check the data file exists to be loaded
		if (File.Exists(Application.persistentDataPath + "/" + _data.SaveFileName + ".frt"))
		{
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/" + _data.SaveFileName + ".frt", FileMode.Open);
		
			_data = (Data)bf.Deserialize(file);
			file.Close();
			
			Debug.Log("Data loaded from " + Application.persistentDataPath + "/" + _data.SaveFileName + ".frt");
			
			return true;
		}
		else
		{
			Debug.LogWarning("Data could not be loaded from " + Application.persistentDataPath + "/" + _data.SaveFileName + ".frt");
			
			return false;
		}
	}
}
