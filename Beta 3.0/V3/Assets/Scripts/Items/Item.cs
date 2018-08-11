/*
  Items
  
  Will Chapman
  Callum Yates
  05/07/2018
  
  Items is an abstract class that other things should inherit from in seperate scripts per item
  
*/

//Using

using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using JetBrains.Annotations;
using UnityEngine;

/// <summary>
/// Describes the type of item a thing is
/// </summary>
public enum ItemType
{
    /// <summary>
    /// Items that improve the player's health in some way
    /// </summary>
    Health,

    /// <summary>
    /// Items that aid the player in combat in some way
    /// </summary>
    Combat,

    /// <summary>
    /// Items that unlock doors
    /// </summary>
    Keys,

    /// <summary>
    /// Items that are collected for the sake of narrative
    /// </summary>
    Collectables

}

/// <summary>
/// The class that all items should inherit from
/// </summary>
public class Item : MonoBehaviour
{
    [SerializeField]
    private string _name;
    [SerializeField]
    private string _description;
    [SerializeField]
    private ItemType _type;
    
    
    public virtual void Start()
    {
    }

    public virtual void Update()
    {
    }

    /// <summary>
    /// The name of the item
    /// </summary>
    public string Name
    {
        get { return _name; }
        set { /*readonly*/ Debug.LogWarning("Cannot set Name. Property is readonly. Try making a new item."); }
    }

    /// <summary>
    /// The description of the item
    /// </summary>
    public string Description
    {
        get { return _description; }
        set { _description = value; }
    }

    /// <summary>
    /// The type of the item
    /// </summary>
    public ItemType Type
    {
        get { return _type; }
        set { _type = value; }
    }

    /// <summary>
    /// Method can be overriden describing action to take when the player picks it up
    /// </summary>
    /// <param name="player">The player object</param>
    public virtual void OnPickup(GameObject player)
    {
    
    }

    /// <summary>
    /// Method to be overriden describing action to take when the player uses it
    /// </summary>
    /// <param name="player">The player object</param>
    public virtual void Use(GameObject player)
    {
    
    }
}