/*
  Virtual Button
  
  Will Chapman
  15/06/2018
  27/06/2018
    
*/

//Using
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Holds all stats for a virtual button
//Inherits from Input Data, sets keycode to keyCode.None as not likely to be used anywhere else
public class VirtualButton : MonoBehaviour
{
      
  /*//////////////////////////////
  ///   Initialise Variables   ///
  //////////////////////////////*/
  
  /// <summary>
  /// The name of the virtual button
  /// </summary>
  [SerializeField] [Tooltip("The name of the virtual button", order = 1)]
  private string _name = "Virtual Button";
  
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
  ///       Class Methods      ///
  //////////////////////////////*/
    
  private void Start()
  {
    _type           =   InputType.Virtual;
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
  /// The name of the virtual button
  /// </summary>
  public string Name
  {
      get { return name; }
      set { /*readonly*/ Debug.LogWarning("Cannot set Name. Property is readonly. Try deleting this Virtual Key and making a new one."); }
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