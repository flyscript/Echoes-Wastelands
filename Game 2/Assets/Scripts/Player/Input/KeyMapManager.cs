/*
  Keymap Manager
  
  Will Chapman
  27/06/2018
  
  
  KeyMap Manager will track user input for you and enable you to request data on
  which keys are currently pressed.
  
  Create a new handler, then add virtual buttons to be tracked. Then add real keys to those virtual ones.
  You can inspect the status of these virtual keys at any time, and add events to fire when the key is pressed,
  goes up, or on every frame.
  
  You are able to see which keys are pressed, and for each of those keys enquire:
  The Key itself
  The Key type (A button, trigger, axis, mouse, touch, accelerometer position)
  Its position (simple keys up/down & positions of XBox triggers, axis, etc)
  The position delta between where it is now and where it was last frame
  The time the key has been in that position
  The time that key was last updated by the Input Handler
  The time delta between this update and the last
  
  Syntax for adding events to key presses is:
  
   KeyMapManager.AddEvent(VirtualKey, InputEventType, delegate (InputData inp)
   {
        ///your code here
   });
     
         
*/

//Using
using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages the use of keymaps
/// </summary>
class KeyMapManager : MonoBehaviour
{
    /*//////////////////////////////
    ///   Initialise Variables   ///
    //////////////////////////////*/
    
    /// <summary>
    /// Pointer to InputHandler
    /// </summary>
    private InputHandler inh;
    
    /// <summary>
    /// Key to current map
    /// </summary>
    private string currentMap;
  
    /// <summary>
    /// Dictionary of all virtual keys
    /// </summary>
    private Dictionary<string, VirtualButton> virtualButtons;
  
    /// <summary>
    /// Dictionary of maps
    /// </summary>
    [SerializeField]
    private Dictionary<string, KeyMap> maps;

    
    
    /*//////////////////////////////
    ///     Initialise Event     ///
    ///       Dictionaries       ///
    //////////////////////////////*/
    
    /// <summary>
    /// Dictionary of event to be fired when virtual button is pushed down
    /// </summary>
    private Dictionary<string, Function>   downEventDict     =   new Dictionary<string, Function>();
    
    /// <summary>
    /// Dictionary of event to be fired when virtual button is let up
    /// </summary>
    private Dictionary<string, Function>   upEventDict       =   new Dictionary<string, Function>();
    
    /// <summary>
    /// Dictionary of event to be fired when virtual button changes position
    /// </summary>
    private Dictionary<string, Function>   changedEventDict  =   new Dictionary<string, Function>();
    
    /// <summary>
    /// Dictionary of event to be fired every MonoBehaviour.Update()
    /// </summary>
    private Dictionary<string, Function>   updateEventDict   =   new Dictionary<string, Function>();
    
    
    
    /*//////////////////////////////
    ///   Define Unity Methods   ///
    //////////////////////////////*/
    
    /// <summary>
    /// Start method called by Unity
    /// </summary>
    void Start()
    {
        //Initialise virtual key dict
        virtualButtons = new Dictionary<string, VirtualButton>();
        
        //Initialise Input Handler
        inh = new InputHandler();
        
        //Initialise maps dict
        maps = new Dictionary<string, KeyMap>(){};

        //Add an empty map
        AddMap("EmptySet");
        currentMap = "EmptySet";
        
        inh.Start();
        
		
        //Create Virtual Keys
        AddVirtualButton("Jump");
        AddVirtualButton("Attack");
        AddVirtualButton("Left");
        AddVirtualButton("Right");
        AddVirtualButton("Pause");
		
        //Create keymap
        AddMap("Default");
        SetTo("Default");

        //Map real keys to virtual keys
        Map().AddKey("Jump", KeyCode.Space, InputType.Button);
		
        Map().AddKey("Attack", KeyCode.F, InputType.Button);
		
        Map().AddKey("Left", KeyCode.A, InputType.Button);
        Map().AddKey("Left", KeyCode.LeftArrow, InputType.Button);
		
        Map().AddKey("Right", KeyCode.D, InputType.Button);
        Map().AddKey("Right", KeyCode.RightArrow, InputType.Button);
		
        Map().AddKey("Pause", KeyCode.P, InputType.Button);
        Map().AddKey("Pause", KeyCode.Escape, InputType.Button);
        
        
    }
    
    /// <summary>
    /// MonoBehaviour updates all keys being tracked every frame
    /// </summary>
    void Update()
    {
        inh.Update();
        
        //For every frame, update the virtual button by going through every key that contributes to it
        //Then fire appropriate events.
        foreach (KeyValuePair<string, List<KeyCode>> virtualRealPair in maps[currentMap].Map)
        {
            //virtualRealPair.Key is the name of the virtual key
            //virtualRealPair.Value is a list of all real keys that contribute to it
            
            if (virtualButtons.ContainsKey(virtualRealPair.Key))
            {
                float[] currentPos = new[] {0f, 0f};
                
                foreach (var realKey in virtualRealPair.Value)
                {
                    //Largest magnitude of position updates the virtual key
                    currentPos[0] = Mathf.Max(new []{inh.Position(realKey)[0], inh.Position(realKey)[0]*-1, currentPos[0]});
                    currentPos[1] = Mathf.Max(new []{inh.Position(realKey)[1], inh.Position(realKey)[1]*-1, currentPos[1]});
                }
                
                //Use status of real key to update status of virtual key
                List<InputEventType> events = virtualButtons[virtualRealPair.Key].Update(currentPos);
                
                //Fire necessary events
                
                //Fire Events
                foreach (var iEvent in events)
                {
                    switch (iEvent)
                    {
                        case InputEventType.Down:
                            if (downEventDict.ContainsKey(virtualRealPair.Key))
                            {
                                downEventDict[virtualRealPair.Key](virtualButtons[virtualRealPair.Key]);
                            }
                            break;
                        case InputEventType.Up:
                            if (upEventDict.ContainsKey(virtualRealPair.Key))
                            {
                                upEventDict[virtualRealPair.Key](virtualButtons[virtualRealPair.Key]);
                            }
                            break;
                        case InputEventType.Changed:
                            if (changedEventDict.ContainsKey(virtualRealPair.Key))
                            {
                                changedEventDict[virtualRealPair.Key](virtualButtons[virtualRealPair.Key]);
                            }
                            break;
                        case InputEventType.Update:
                            if (updateEventDict.ContainsKey(virtualRealPair.Key))
                            {
                                updateEventDict[virtualRealPair.Key](virtualButtons[virtualRealPair.Key]);
                            }
                            break;
                    }
                }
            }
        }
    }
    
    
    
    /*//////////////////////////////
    ///  Virtual Button Methods  ///
    //////////////////////////////*/
    
    /// <summary>
    /// Creates a virtual button
    /// </summary>
    /// <param name="_name">The name of the virtual button</param>
    public void AddVirtualButton(string _name)
    {
        if (virtualButtons.ContainsKey(_name))
        {
            Debug.LogWarning("Virtual key \""+ _name + "\" already exists! Aborting Attempt");
        }
        else
        {
            virtualButtons.Add(_name, new VirtualButton(_name));
        }
    }
    
    /// <summary>
    /// Removes a virtual button
    /// </summary>
    /// <param name="_name">The name of the button to be removed</param>
    public void RemoveVirtualButton(string _name)
    {
        if (virtualButtons.ContainsKey(_name))
        {
            virtualButtons.Remove(_name);
        }
        else
        {
            Debug.LogWarning("Virtual key \""+ _name + "\" does not exist! Aborting Attempt");
        }
    }
  
    /// <summary>
    /// Method to add a custom event function to be fired when a key meets the InputEventType
    /// </summary>
    /// <param name="_virtualKey">The key to have the event connected to</param>
    /// <param name="_eventType">The type of event, when it is fired</param>
    /// <param name="_function">The code to be fired, has input data passed as first argument</param>
    public void AddEvent(String _virtualKey, InputEventType _eventType, Function _function)
    {
        switch (_eventType)
        {
            case InputEventType.Down:
                downEventDict.Add(_virtualKey, _function);
                break;
            case InputEventType.Up:
                upEventDict.Add(_virtualKey, _function);
                break;
            case InputEventType.Changed:
                changedEventDict.Add(_virtualKey, _function);
                break;
            case InputEventType.Update:
                updateEventDict.Add(_virtualKey, _function);
                break;
            default:
                Debug.Log("Event did not exist! Aborting attempt");
                break;
        }
    }
    
    
    
    /*//////////////////////////////
    ///        Map Methods       ///
    //////////////////////////////*/
    
    /// <summary>
    /// Returns the inputHandler
    /// </summary>
    /// <returns>Input Handler</returns>
    public InputHandler InputHandler()
    {
        return inh;
    }
    
    /// <summary>
    /// Returns the current map
    /// </summary>
    /// <returns>KeyMap</returns>
    public KeyMap Map()
    {
        return maps[currentMap];
    }
    
    /// <summary>
    /// Sets the current map
    /// </summary>
    /// <param name="_name">The name of the map to be made current</param>
    public void SetTo(string _name)
    {
        if (maps.ContainsKey(_name))
        {
            currentMap = _name;
        }
        else
        {
            Debug.LogWarning("Map \""+ _name + "\" does not exist! Aborting Attempt");
        }
    }
  
    /// <summary>
    /// Adds a new keymap to the manager
    /// </summary>
    /// <param name="_name">The name of the new map</param
    public void AddMap(string _name)
    {
        if (maps.ContainsKey(_name))
        {
            Debug.LogWarning("Map \""+ _name + "\" already exists! Aborting Attempt");
        }
        else
        {
            maps.Add(_name, new KeyMap(_name, this, virtualButtons));
        }
    }
  
    /// <summary>
    /// Removes a keymap from the manager
    /// </summary>
    /// <param name="_name">The name of the map to be removed</param>
    public void RemoveMap(string _name)
    {
        if (maps.ContainsKey(_name))
        {
            if (maps.Count == 1)
            {
                Debug.LogWarning("Map \""+ _name + "\" is the last map in the manager! Manager cannot be empty! Aborting Attempt");
            }
            else
            {
                maps.Remove(_name);
            }
        }
        else
        {
            Debug.LogWarning("Map \""+ _name + "\" does not exist! Aborting Attempt");
        }
    }
}
