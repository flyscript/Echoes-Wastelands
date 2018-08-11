/*
  Input Data
  
  Will Chapman
  14/06/2018
  28/06/2018
  
  InputData is a class containing stasticial details of a tracked key, managed by InputManager
  
  Also contained here are the InputType and InputEventType Enums.
  
*/

//Using
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

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
  [SerializeField] [Tooltip("The KeyCode of the input", order = 2)]
  private KeyCode _input;
  
  /// <summary>
  /// The type of the input
  /// </summary>
  [SerializeField] [Tooltip("The type of the input", order = 3)]
  private InputType _type;
  
  /// <summary>
  /// The position of the input
  /// </summary>
  [SerializeField] [Tooltip("The position of the input", order = 4)]
  private float[] _position;
  
  /// <summary>
  /// The delta position between last frame and this frame
  /// </summary>
  [SerializeField] [Tooltip("The delta position between last frame and this frame", order = 5)]
  private float[] _positionDelta;
  
  /// <summary>
  /// The time the key has been in its current position
  /// </summary>
  [SerializeField] [Tooltip("The time the key has been in its current position", order = 6)]
  private float _positionTime;
  
  /// <summary>
  /// The total time the key has been tracked for
  /// </summary>
  [SerializeField] [Tooltip("The total time the key has been tracked for", order = 7)]
  private float _timeTracked;
  
  /// <summary>
  ///The timestamp of the last update
  /// </summary>
  [SerializeField] [Tooltip("The timestamp of the last update", order = 8)]
  private float _lastUpdate;
    
  
  
  
  /*//////////////////////////////
  ///      Class Methods       ///
  //////////////////////////////*/
  
  /// <summary>
  /// Constructor creates new InputData
  /// </summary>
  /// <param name="keyIdentifier">The identifier of the key</param>
  /// <param name="type">The type of the input</param>
  public InputData(KeyCode keyIdentifier, InputType type )
  {
    _input          =   keyIdentifier;
    _type           =   type;
    _position       =   new [] {0f, 0f};
    _positionDelta  =   new [] {0f, 0f};
    _positionTime   =   0;
    _timeTracked    =   0;
    _lastUpdate     =   Time.time;
  }
  
  
  
  /*//////////////////////////////
  ///          Methods         ///
  //////////////////////////////*/
  
  /// <summary>
  /// The keycode of the input
  /// </summary>
  public KeyCode Input
  {
    get { return _input; }
    //TODO: Need to update this to start logging new keycode in inputmanager
    set { _input = value; }
  }
  
  /// <summary>
  /// The type of the input
  /// </summary>
  public InputType Type
  {
    get { return _type; }
    set { _type = value; }
  }
  
  /// <summary>
  /// The position of the input
  /// </summary>
  public float[] Position
  {
    get { return _position; }
    set { /*readonly*/ Debug.LogWarning("Cannot set POSITION. Property is readonly"); }
  }
  
  /// <summary>
  /// The delta position between last frame and this frame
  /// </summary>
  public float[] PositionDelta
  {
    get { return _positionDelta; }
    set { /*readonly*/ Debug.LogWarning("Cannot set POSITIONDELTA. Property is readonly"); }
  }
  
  /// <summary>
  /// The time the key has been in its current position
  /// </summary>
  public float PositionTime
  {
    get { return _positionTime; }
    set { /*readonly*/ Debug.LogWarning("Cannot set POSITIONTIME. Property is readonly"); }
  }
  
  /// <summary>
  /// The total time the key has been tracked for
  /// </summary>
  public float TimeTracked
  {
    get { return _timeTracked; }
    set { /*readonly*/ Debug.LogWarning("Cannot set TIMETRACKED. Property is readonly"); }
  }
  
  /// <summary>
  /// The timestamp of the last update
  /// </summary>
  public float LastUpdate
  {
    get { return _lastUpdate; }
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
  public List<InputEventType> InputUpdate(float[] _newPosition)
  {
    List<InputEventType> events = new List<InputEventType>();
    
    //Update position delta
    _positionDelta[0] = _newPosition[0] - Position[0];
    _positionDelta[1] = _newPosition[1] - Position[1];
    
    //Update position
    _position = _newPosition;
    
    //Update Position Time and add events
    if (_positionDelta[0] == 0 && _positionDelta[1] == 0f)
    {
      _positionTime += Time.deltaTime;
    }
    else
    {
      if (_positionDelta[0] > 0 && _position[0] == 1f ) events.Add(InputEventType.Down);
      if (_positionDelta[0] < 0 && _position[0] == 0f ) events.Add(InputEventType.Up);

      events.Add(InputEventType.Changed);
    }
    events.Add(InputEventType.Update);
    
    //Update Time Tracked
    _timeTracked += Time.deltaTime;
    
    //Update LastUpdate
    _lastUpdate = Time.time;
    
    //Return events
    return events;
  }
      
}