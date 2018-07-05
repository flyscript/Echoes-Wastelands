/*
  Keymap
  
  Will Chapman
  15/06/2018
  27/06/2018
    
*/

//Using
using System.Collections.Generic;
using UnityEngine;

//Maps real keys to virtual keys, and connects real key events to their virtual counterparts
//Metaphorically: The class is github, the virtual key is a project, the real keys are contributors
class KeyMap
{
  /*//////////////////////////////
  ///   Initialise Variables   ///
  //////////////////////////////*/
  
  /// <summary>
  /// Name of this keymap
  /// </summary>
  private string name;
  
  /// <summary>
  /// Reference to KeyMapManager
  /// </summary>
  private KeyMapManager manager;

  /// <summary>
  /// Dictionary holding list of all keys that contribute to the virtual key
  /// </summary>
  private Dictionary<string, List<KeyCode>> map;

  
  
  /*//////////////////////////////
  ///       Class Methods      ///
  //////////////////////////////*/
  
  /// <summary>
  /// Creates new Key Map
  /// </summary>
  /// <param name="_name">The name of the keymap</param>
  /// <param name="_manager">A reference to the KeyMapManager</param>
  /// <param name="_virtuals">A reference of all virtual keys</param>
  public KeyMap(string _name, KeyMapManager _manager, Dictionary<string, VirtualButton> _virtuals)
  {
    //Set name
    name = _name;
    
    //Set manager
    manager = _manager;
    
    //Populate map
    map = new Dictionary<string, List<KeyCode>>();

    foreach (KeyValuePair<string, VirtualButton> _virtual in _virtuals)
    {
      map.Add(_virtual.Key, new List<KeyCode>());
    }
  }
  
  
  
  /*//////////////////////////////
  ///          Methods         ///
  //////////////////////////////*/

  /// <summary>
  /// Returns the map of real keys to virtual keys
  /// </summary>
  public Dictionary<string, List<KeyCode>> Map
  {
    get { return map; }
    set { /*readonly*/ Debug.LogWarning("Cannot set Map. Property is readonly. Try making a new map."); }
  }
  
  /// <summary>
  /// Adds a real key to a virtual key
  /// </summary>
  /// <param name="_virtual">Virtual key</param>
  /// <param name="_key">Real key to add</param>
  /// <param name="_type">Type of real key to add</param>
  public void AddKey(string _virtual, KeyCode _key, InputType _type)
  {
    if (map.ContainsKey(_virtual))
    {
      if (map[_virtual].Contains(_key))
      {
        Debug.LogWarning("Virtual key \""+ _virtual + "\" already contains this key! Aborting Attempt");
      }
      else
      {
        manager.InputHandler().TrackInput(_key, _type);
        map[_virtual].Add(_key);
      }
    }
    else
    {
      Debug.LogWarning("Virtual key \""+ _virtual + "\" does not exist! Aborting Attempt");
    }
  }
  
  /// <summary>
  /// Removes a real key from a virtual key
  /// </summary>
  /// <param name="_virtual">Virtual key</param>
  /// <param name="_key"></param>
  public void RemoveKey(string _virtual, KeyCode _key)
  {
    if (map.ContainsKey(_virtual))
    {
      if (map[_virtual].Contains(_key))
      {
        map[_virtual].Remove(_key);
      }
      else
      {
        Debug.LogWarning("Virtual key \""+ _virtual + "\" does not contain this key! Aborting Attempt");
      }
    }
    else
    {
      Debug.LogWarning("Virtual key \""+ _virtual + "\" does not exist! Aborting Attempt");
    }
  }
}