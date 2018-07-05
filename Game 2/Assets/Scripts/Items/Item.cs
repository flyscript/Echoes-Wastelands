/*
  Items
  
  Will Chapman
  05/07/2018
  
  Items is an abstract class that other things should inherit from in seperate scripts per item
  
*/

//Using
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Boo.Lang;
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
public class Item
{
    private string name;
    private string description;
    private ItemType type;

    protected Item(string _name, string _description, ItemType _type)
    {
        name = _name;
        description = _description;
        type = _type;
    }
    
    
}