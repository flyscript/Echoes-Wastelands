/*
  Input Data
  
  Will Chapman
  14/06/2018
  28/06/2018
  
  InputData is a class containing stasticial details of a tracked key, managed by InputManager
  
  Also contained here are the InputType and InputEventType Enums, as well as the delegate function for making events.
  
*/

//Using
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;


/// <summary>
/// Enum describing the input types
/// </summary>
public enum InputType
{
  /// <summary>
  /// A key or button is a binary key of only 2 positions. A standard key.
  /// </summary>
  Button,
  
  /// <summary>
  /// An axis is a 2-dimensional analogue input, like a thumbstick.
  /// </summary>
  Axis,
  
  /// <summary>
  /// A trigger is a 1-dimensional analogue button, like the triggers on an XInput Device
  /// </summary>
  Trigger,
  
  /// <summary>
  /// A mouse is a 2-dimensional digital input whose position is given in a percentage of the screen
  /// </summary>
  Mouse,
  
  /// <summary>
  /// A touch is a 2-dimensional digital input whose position is given as an exact screen position
  /// </summary>
  Touch,
  
  /// <summary>
  /// An accelerometer is a 3-dimensional input that is tracked by the accelerometer-enabled device
  /// </summary>
  Accelerometer,
  
  /// <summary>
  /// A virtual key, used by KeyMaps and KeyMapManager
  /// </summary>
  Virtual
}

/// <summary>
/// Enum describing the input event type
/// </summary>
public enum InputEventType
{
  /// <summary>
  /// The input was pressed all the way
  /// </summary>
  Down,
  
  /// <summary>
  /// The input was returned to its normal position
  /// </summary>
  Up,
  
  /// <summary>
  /// The input changed position
  /// </summary>
  Changed,
  
  /// <summary>
  /// Fires every frame
  /// </summary>
  Update
}


/// <summary>
/// A blank delegate to insert anonymous methods with for events
/// </summary>
/// <param name="input">The inputdata for the tracked key</param>
public delegate void Function(InputData _input);


/// <summary>
/// Class containing all the data for a single input instance
/// </summary>
public class InputData
{
  
  /*//////////////////////////////
  ///   Initialise Variables   ///
  //////////////////////////////*/
  
  
  /// <summary>
  /// The KeyCode of the input
  /// </summary>
  private KeyCode input;
  
  /// <summary>
  /// The type of the input
  /// </summary>
  private InputType type;
  
  /// <summary>
  /// The position of the input
  /// </summary>
  private float[] position;
  
  /// <summary>
  /// The delta position between last frame and this frame
  /// </summary>
  private float[] positionDelta;
  
  /// <summary>
  /// The time the key has been in its current position
  /// </summary>
  private float positionTime;
  
  /// <summary>
  /// The total time the key has been tracked for
  /// </summary>
  private float timeTracked;
  
  /// <summary>
  ///The timestamp of the last update
  /// </summary>
  private float lastUpdate;
    
  
  
  
  /*//////////////////////////////
  ///      Class Methods       ///
  //////////////////////////////*/
  
  /// <summary>
  /// Constructor creates new InputData
  /// </summary>
  /// <param name="_keyIdentifier">The identifier of the key</param>
  /// <param name="_type">The type of the input</param>
  public InputData(KeyCode _keyIdentifier, InputType _type )
  {
    input          =   _keyIdentifier;
    type           =   _type;
    position       =   new []{0f, 0f};
    positionDelta  =   new [] {0f, 0f};
    positionTime   =   0;
    timeTracked    =   0;
    lastUpdate     =   Time.time;
  }
  
  
  
  /*//////////////////////////////
  ///          Methods         ///
  //////////////////////////////*/
  
  /// <summary>
  /// The keycode of the input
  /// </summary>
  public KeyCode Input
  {
    get { return input; }
    set { /*readonly*/ Debug.LogWarning("Cannot set INPUT. Property is readonly"); }
  }
  
  /// <summary>
  /// The type of the input
  /// </summary>
  public InputType Type
  {
    get { return type; }
    set { /*readonly*/ Debug.LogWarning("Cannot set TYPE. Property is readonly"); }
  }
  
  /// <summary>
  /// The position of the input
  /// </summary>
  public float[] Position
  {
    get { return position; }
    set { /*readonly*/ Debug.LogWarning("Cannot set POSITION. Property is readonly"); }
  }
  
  /// <summary>
  /// The delta position between last frame and this frame
  /// </summary>
  public float[] PositionDelta
  {
    get { return positionDelta; }
    set { /*readonly*/ Debug.LogWarning("Cannot set POSITIONDELTA. Property is readonly"); }
  }
  
  /// <summary>
  /// The time the key has been in its current position
  /// </summary>
  public float PositionTime
  {
    get { return positionTime; }
    set { /*readonly*/ Debug.LogWarning("Cannot set POSITIONTIME. Property is readonly"); }
  }
  
  /// <summary>
  /// The total time the key has been tracked for
  /// </summary>
  public float TimeTracked
  {
    get { return timeTracked; }
    set { /*readonly*/ Debug.LogWarning("Cannot set TIMETRACKED. Property is readonly"); }
  }
  
  /// <summary>
  /// The timestamp of the last update
  /// </summary>
  public float LastUpdate
  {
    get { return lastUpdate; }
    set { /*readonly*/ Debug.LogWarning("Cannot set LASTUPDATE. Property is readonly"); }
  }
  
  /// <summary>
  /// The time delta between the last update and now
  /// </summary>
  public float UpdateDelta
  {
    get { return Time.time - LastUpdate; }
    set { /*readonly*/ Debug.LogWarning("Cannot set UPDATEDELTA. Property is readonly"); }
  }
  
  /// <summary>
  /// Called by InputHandler, should not be invoked by user
  /// </summary>
  /// <param name="_newPosition">The new position of this input</param>
  public List<InputEventType> Update(float[] _newPosition)
  {
    List<InputEventType> events = new List<InputEventType>();
    
    //Update position delta
    positionDelta[0] = _newPosition[0] - Position[0];
    positionDelta[1] = _newPosition[1] - Position[1];
    
    //Update position
    position = _newPosition;
    
    //Update Position Time and add events
    if (positionDelta[0] == 0 && positionDelta[1] == 0f)
    {
      positionTime += Time.deltaTime;
    }
    else
    {
      if (positionDelta[0] > 0 && position[0] == 1f ) events.Add(InputEventType.Down);
      if (positionDelta[0] < 0 && position[0] == 0f ) events.Add(InputEventType.Up);

      events.Add(InputEventType.Changed);
    }
    events.Add(InputEventType.Update);
    
    //Update Time Tracked
    timeTracked += Time.deltaTime;
    
    //Update LastUpdate
    lastUpdate = Time.time;
    
    //Return events
    return events;
  }
      
}