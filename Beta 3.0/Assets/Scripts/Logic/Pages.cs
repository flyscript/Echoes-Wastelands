/*
  Pages
  
  Will Chapman
  09/08/2018
  
  
*/

//Using
using System;
using Boo.Lang;
using UnityEngine;

/// <summary>
/// Page behaviour
/// </summary>
[Serializable]
public class Page
{

  [SerializeField]
  private string _levelName;
  [SerializeField]
  private string _levelDescription;
  [SerializeField]
  private int _levelDeaths;
  [SerializeField]
  private List<bool> _gems = new List<bool>(){false, false, false, false};
  private bool _gem1;
  [SerializeField]
  private bool _gem2;
  [SerializeField]
  private bool _gem3;
  [SerializeField]
  private bool _gem4;


  public Page(string name, string desc)
  {
      _levelName = name;
     _levelDescription = desc;
  }
  
  /// <summary>
  /// The name of the item
  /// </summary>
  public string Name
  {
    get { return _levelName; }
    set { /*readonly*/ Debug.LogWarning("Cannot set Name. Property is readonly. Try making a new item."); }
  }

  /// <summary>
  /// The description of the item
  /// </summary>
  public string Description
  {
    get { return _levelDescription; }
    set { _levelDescription = value; }
  }

  public int LevelDeaths
  {
    get { return _levelDeaths; }
    set { _levelDeaths = value; }
  }

  public List<bool> Gems
  {
    get { return _gems; }
    set { _gems = value; }
  }
    
}