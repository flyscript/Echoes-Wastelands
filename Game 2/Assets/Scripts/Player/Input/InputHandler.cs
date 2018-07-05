/*
  Input Handler
  
  Will Chapman
  14/06/2018
  27/06/2018
  
    
  Everything in Scripts/Player/Input is taken from last module. Needs to be refactored to take Controller Input too.
  
  Input Handler will track user input for you and enable you to request data on
  which keys are currently pressed.
  
  Create a new handler, then add keys to be tracked. You can inspect the status of these keys at any time, and add
  events to fire when the lkey is pressed, goes up, or on every frame.
  
  You are able to see which keys are pressed, and for each of those keys enquire:
  The Key itself
  The Key type (A button, trigger, axis, mouse, touch, accelerometer position)
  Its position (simple keys up/down & positions of XBox triggers, axis, etc)
  The position delta between where it is now and where it was last frame
  The time the key has been in that position
  The time that key was last updated by the Input Handler
  The time delta between this update and the last
  
  Syntax for adding events to key presses is:
  
	   InputHandler.AddEvent(KeyCode, InputEventType, delegate (InputData inp)
	   {
	   
     });
     
     
     NOTE: DO NOT USE FOR PLAYER INPUT - USE KEYMAP MANAGER INSTEAD
  
*/

//Using
using System.Collections.Generic;
using UnityEngine;


/*
The main class. Should only be instantiated once; will track all input for you, make requests to it
Only use one otherwise the redundant duplicates will just eat memory and CPU time.
*/
class InputHandler
{
  /*//////////////////////////////
  ///   Initialise Variables   ///
  //////////////////////////////*/
  
  /// <summary>
  /// Dictionary of inputs to be tracked
  /// </summary>
  private Dictionary<KeyCode, InputData> inputDict;
  
  
  
  /*//////////////////////////////
  ///     Initialise Event     ///
  ///       Dictionaries       ///
  //////////////////////////////*/
  
  /// <summary>
  /// Dictionary of events when key is pressed down
  /// </summary>
  private Dictionary<KeyCode, Function>  downEventDict;
  
  /// <summary>
  /// Dictionary of events when key is let up
  /// </summary>
  private Dictionary<KeyCode, Function>  upEventDict;
  
  /// <summary>
  /// Dictionary of events when key changes position
  /// </summary>
  private Dictionary<KeyCode, Function>  changedEventDict;
  
  /// <summary>
  /// Dictionary of events every MonoBehaviour.Update()
  /// </summary>
  private Dictionary<KeyCode, Function>  updateEventDict;
  
  
  
  /*//////////////////////////////
  ///      Unity Methods       ///
  //////////////////////////////*/
  
  /// <summary>
  /// Creates an input handler
  /// </summary>
  public void Start()
  {
    //Initialise inputDict
    inputDict = new Dictionary<KeyCode, InputData>();

    //Initialise events
    downEventDict = new Dictionary<KeyCode, Function>();
    upEventDict = new Dictionary<KeyCode, Function>();
    changedEventDict = new Dictionary<KeyCode, Function>();
    updateEventDict = new Dictionary<KeyCode, Function>();
  }
  
  /// <summary>
  /// MonoBehaviour updates all keys being tracked every frame
  /// </summary>
  public void Update ()
  {
    foreach(KeyValuePair<KeyCode, InputData> input in inputDict)
    {
      // do something with input.Value or input.Key
      //Debug.Log("Updating Input Dictionary ["+input.Key+"]: ", input.Value.ToString());

      //Initialise a variable to contain the events to be fired after the update
      List<InputEventType> events = new List<InputEventType>();
      
      switch (input.Value.Type)
      {
        case InputType.Button:
          //Key Down
          if (Input.GetKey(input.Key))
          {
            events = inputDict[input.Key].Update(new []{1f, 0f});
          }
          //Key Up
          else
          {
            events = inputDict[input.Key].Update(new []{0f, 0f});
          }
          break;
        
        case InputType.Axis:
          /*
           input.Value.Position[0] = UnityInput.GetAxisPos(input.Key)[0];
           */
          break;
        
        case InputType.Trigger:
          break;
        
        case InputType.Mouse:
          break;
        
        case InputType.Touch:
          break;
        
        case InputType.Accelerometer:
          break;
        
        default:
          Debug.LogWarning("INPUT SYSTEM ERROR \"Update\": key has invalid type");
          break;
      }

      //Fire Events
      foreach (var iEvent in events)
      {
        switch (iEvent)
        {
          case InputEventType.Down:
            if (downEventDict.ContainsKey(input.Key))
            {
              downEventDict[input.Key](input.Value);
            }
            break;
          case InputEventType.Up:
            if (upEventDict.ContainsKey(input.Key))
            {
              upEventDict[input.Key](input.Value);
            }
            break;
          case InputEventType.Changed:
            if (changedEventDict.ContainsKey(input.Key))
            {
              changedEventDict[input.Key](input.Value);
            }
            break;
          case InputEventType.Update:
            if (updateEventDict.ContainsKey(input.Key))
            {
              updateEventDict[input.Key](input.Value);
            }
            break;
        }
      }
    }
  }
  
  
    
  /*//////////////////////////////
  ///          Methods         ///
  //////////////////////////////*/
  
  /// <summary>
  /// Method to add input to the dictionary for tracking
  /// </summary>
  /// <param name="_keyIdentifier">KeyCode to be tracked</param>
  /// <param name="_inputType">The type of input it is</param>
  public void TrackInput(KeyCode _keyIdentifier, InputType _inputType)
  {
    Debug.Log("Received Track Request");
    inputDict.Add(
      _keyIdentifier,
      new InputData(_keyIdentifier, _inputType)
    );
  }
  
  /// <summary>
  /// Method to remove input from the dictionary
  /// </summary>
  /// <param name="_keyIdentifier">KeyCode to be removed from tracking</param>
  public void IgnoreInput(KeyCode _keyIdentifier)
  {
    if (inputDict.ContainsKey(_keyIdentifier))
    {
      inputDict.Remove(_keyIdentifier); 
    }
    else
    {
      Debug.Log("Key did not exist! Aborting attempt");
    }
  }

  /// <summary>
  /// Method to get status of a certain key
  /// </summary>
  /// <param name="_keyIdentifier">The key to get the status of</param>
  /// <returns></returns>
  public InputData GetState(KeyCode _keyIdentifier)
  {
    if (inputDict.ContainsKey(_keyIdentifier))
    {
      return inputDict[_keyIdentifier];
    }
    else
    {
      Debug.Log("Key did not exist! Aborting attempt");
      return null;
    }
  }
    
  /// <summary>
  /// Method to get just position of a certain key
  /// </summary>
  /// <param name="_keyIdentifier">The key to get the position of</param>
  /// <returns></returns>
  public float[] Position(KeyCode _keyIdentifier)
  {
    if (inputDict.ContainsKey(_keyIdentifier))
    {
      return inputDict[_keyIdentifier].Position;
    }
    else
    {
      Debug.Log("Key did not exist! Aborting attempt");
      return null;
    }
  }
  
  /// <summary>
  /// Method to add a custom event function to be fired when a key meets the InputEventType
  /// </summary>
  /// <param name="_keyIdentifier">The key to have the event connected to</param>
  /// <param name="_eventType">The type of event, when it is fired</param>
  /// <param name="_function">The code to be fired, has input data passed as first argument</param>
  public void AddEvent(KeyCode _keyIdentifier, InputEventType _eventType, Function _function)
  {
    switch (_eventType)
    {
      case InputEventType.Down:
        downEventDict.Add(_keyIdentifier, _function);
        break;
      case InputEventType.Up:
        upEventDict.Add(_keyIdentifier, _function);
        break;
      case InputEventType.Changed:
        changedEventDict.Add(_keyIdentifier, _function);
        break;
      case InputEventType.Update:
        updateEventDict.Add(_keyIdentifier, _function);
        break;
      default:
        Debug.Log("Event did not exist! Aborting attempt");
        break;
    }
  }
  
}















