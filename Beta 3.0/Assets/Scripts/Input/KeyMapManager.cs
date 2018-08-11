/*
  Keymap Manager
  
  Will Chapman
  10/08/2018
  
  WHAT THIS DOES:
      KeyMap Manager will track virtual keys for you and automate the user input capture process, enabling
      you to request data on which virtual keys are currently pressed and the status of them.
  
  WHY IT MATTERS:
      Instead of relying on simple if statements run every Update in a MonoBehaviour which can result in
      unexpected and game-breaking behaviour, this code allows you to expertly monitor the status of keys for
      reliable and predictable results.
      
      This system puts more power in the hands of the developer and gives them more statistics about player input.
      
      It also enables the developer to create Virtual Keys to be tracked that can be mapped to many real
      keys, in an automated process abstracted away from them that they don't have to worry about. This means you can
      have as many keys as you want be the 'Fire' button, for example, without the nede for lengthy bespoke code that
      hinders development.
      
      MOST IMPORTANTLY, the addition of custom events that can be fired in many different scenarios (Every frame; or
      when a key goes down, up, or changes) automatically for every virtual key mean that utilising the player's input
      is more streamlined than ever, and can result in much cleaner code that doesn't rely on unpredictable Update()
      if statement behaviour!
  
  
  ---------------------------------------------------------------------
  |          IF YOUR CODE ISN'T WORKING THEN CHECK YOU ARE            |
  |              PROPERLY FOLLOWING THESE INSTRUCTIONS.               |
  |                                                                   |
  |                     GARBAGE IN = GARBAGE OUT                      |
  ---------------------------------------------------------------------


  --Set Up--
  
  >Ensure that the Key Map Manager component of InputSystem has a map from InputSystems>Maps wired into its CurrentMap property,
  and that its VirtualButtons property has InputSystem>VirtualButtons wired into it.
  >The KeyMapManager automatically makes itself a Singleton BUT DOES NOT PERSIST.
  >To access the KeyMapManager from anywhere in your code in the current scene, simply do KeyMapManager.manager
  
  
  
  --Making Virtual Buttons--
  
  >Under InputSystem>VirtualButtons, add as many virtual buttons as your game is going to need.
  >These should be the core functional input aspects to the game, such as Fire or Jump or Sprint.
  
  
  
  --Making Key Maps--
  
  >You can create as many maps as you want, which should be parented to InputSystem>Maps.
  >These are blank GameObjects that contain many Virtual Button Key Pair components.
  
  
  
  --Mapping Real Keys to Virtual ones in a Map--
  
  >To map real keys to a virtual one, create a single Virtual Button Key Pair component for that virtual button.
  >Wire a Virtual Button object you made, to be found under InputSystem>VirtualButtons, into its Virtual Button property.
  >Expand the RealKeys property if it isn't already, and edit the Size element to reflect the number of real keys you want
  to contribute to this virtual key. This could just be 1, or over 100.
  >You will get a list of Elements that appear to reflect the number you just entered. Each one is a dropdown that will let
  you select a real key.
  >Select a key for each of these elements to contribute to the virtual key.
  
  
  
  --Accessing VirtualButton Data--
  
  >VirtualButtons contain many bits of data that developers may find useful.
  >Here is what can be found:
    Name
        The string name of the VirtualButton
    
    Type
        The type of the VirtualButton. Virtual by default, but can be set to something else for your own purposes. Does not affect behaviour.
    
    Position    
        Its position as a 1Dimensional float array where [0] is X axis and [1] is Y axis (Buttons just use X or [0])
    
    PositionDelta    
        The position delta between where it is now and where it was last frame in the same format as above
    
    PositionTime 
        A float describing the time the key has been in that position
        
    TimeTracked
        A float describing how long this virtual button has been updated
        
    LastUpdate
        A float describing the time that key was last updated by the Input Handler
        
    UpdateDelta
        A float describing the time delta between this update and the last
        
    There is also a method called InputUpdate() which takes the new position of the virtual button as its input.
        This should NEVER be used unless you really know what you're doing. This is a method called automatically
        by the program after computing what the position of the virtual button should be before updating its stats.
        Messing with this can have serious consequences.
  
  
  
  --Creating an Event--
  
  >To create an event you need only refer to the Key Map Manager through KeyMapManager.manager and use the Addevent method.
  >The AddEvent method has 3 variables. The virtual key that should activate the event, the type of key action that should
  activate that event, and the code to be run when that event fires.
  >All code in the event is definition-scoped, so it will use the scope of wherever it was defined and all accessible
  variables thereof.
  >The function you define has an argument sent to it, which is the VirtualButton itself, which contains the parameters
  specified above in --Accessing VirtualButton Data--
  >The syntax is:
  
       [KeyMapManager].AddEvent(string virtualKeyName, InputEventType eventType, delegate (VirtualButton virtualButton)
       {
            //your code here
       });
       
  >An example of this:
  
		KeyMapManager.manager.AddEvent("Jump", InputEventType.Down, delegate (VirtualButton virtualButton)
		{
		  //Show the button was pressed
	      Debug.Log("TEST BUTTON DOWN!");
	      
	      ///Show the positionDelta of the virtual button
	      Debug.Log(virtualButton.PositionDelta);
		});
  
  
  
  ---- Have fun! ----
  
  
  
  --
  Will Chapman
  
     
         
*/

//Using
using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A blank delegate to insert anonymous methods with for events
/// </summary>
/// <param name="input">The inputdata for the tracked key</param>
public delegate void VirtualFunction(VirtualButton _input);


/// <summary>
/// Manages the use of keymaps
/// </summary>
class KeyMapManager : MonoBehaviour
{
    /*//////////////////////////////
    ///   Initialise Variables   ///
    //////////////////////////////*/

    public static KeyMapManager manager;
    
    /// <summary>
    /// Pointer to InputHandler
    /// </summary>
    private InputHandler _inh = new InputHandler();
    
    /// <summary>
    /// Key to current map
    /// </summary>
    [SerializeField] [Tooltip("The current map")]
    private GameObject _currentMap;
  
    /// <summary>
    /// Object containing all virtual buttons as components
    /// </summary>
    [SerializeField] [Tooltip("The object containing all virtual buttons")]
    private GameObject _virtualButtons;
  
    /*//////////////////////////////
    ///     Initialise Event     ///
    ///       Dictionaries       ///
    //////////////////////////////*/
    
    /// <summary>
    /// Dictionary of event to be fired when virtual button is pushed down
    /// </summary>
    [SerializeField]
    private Dictionary<string, VirtualFunction>   downEventDict     =   new Dictionary<string, VirtualFunction>();
    
    /// <summary>
    /// Dictionary of event to be fired when virtual button is let up
    /// </summary>
    [SerializeField]
    private Dictionary<string, VirtualFunction>   upEventDict       =   new Dictionary<string, VirtualFunction>();
    
    /// <summary>
    /// Dictionary of event to be fired when virtual button changes position
    /// </summary>
    [SerializeField]
    private Dictionary<string, VirtualFunction>   changedEventDict  =   new Dictionary<string, VirtualFunction>();
    
    /// <summary>
    /// Dictionary of event to be fired every MonoBehaviour.Update()
    /// </summary>
    [SerializeField]
    private Dictionary<string, VirtualFunction>   updateEventDict   =   new Dictionary<string, VirtualFunction>();
    
    
    
    /*//////////////////////////////
    ///   Define Unity Methods   ///
    //////////////////////////////*/
    
    
    /// <summary>
    /// Awake method called by Unity
    /// </summary>
    void Awake()
    {
        manager = this;
        
        //Add keys to _inh as necessary:
        //Loop through every virtual/real pair
        foreach (var vrp in gameObject.GetComponentsInChildren<VirtualButtonKeyPairs>())
        {
            //Loop through every real key in the pair
            foreach (var key in vrp.RealKeys)
            {
                _inh.TrackInput(key, InputType.Button);
            }
        }
    }
    
    /// <summary>
    /// MonoBehaviour updates all keys being tracked every frame
    /// </summary>
    void Update()
    {
        _inh.Update();
        
        //For every frame, update the virtual button by going through every key that contributes to it
        //Then fire appropriate events.
        foreach (VirtualButtonKeyPairs virtualRealPair in _currentMap.GetComponents<VirtualButtonKeyPairs>())
        {
            
            if (virtualRealPair.VirtualButton!=null)
            {
                float[] currentPos = new[] {0f, 0f};
                
                foreach (var realKey in virtualRealPair.RealKeys)
                {
                    //Largest magnitude of position updates the virtual key
                    currentPos[0] = Mathf.Max(new []{_inh.Position(realKey)[0], _inh.Position(realKey)[0]*-1, currentPos[0]});
                    currentPos[1] = Mathf.Max(new []{_inh.Position(realKey)[1], _inh.Position(realKey)[1]*-1, currentPos[1]});
                }
                
                //Update virtual button and use status of real key to update status of virtual key
                List<InputEventType> events = virtualRealPair.VirtualButton.GetComponent<VirtualButton>().InputUpdate(currentPos);
                
                //Fire necessary events
                
                //Fire Events
                foreach (var iEvent in events)
                {
                    switch (iEvent)
                    {
                        case InputEventType.Down:
                            if (downEventDict.ContainsKey(virtualRealPair.VirtualButton.GetComponent<VirtualButton>().Name))
                            {
                                downEventDict[virtualRealPair.VirtualButton.GetComponent<VirtualButton>().Name](virtualRealPair.VirtualButton.GetComponent<VirtualButton>());
                            }
                            break;
                        case InputEventType.Up:
                            if (upEventDict.ContainsKey(virtualRealPair.VirtualButton.GetComponent<VirtualButton>().Name))
                            {
                                upEventDict[virtualRealPair.VirtualButton.GetComponent<VirtualButton>().Name](virtualRealPair.VirtualButton.GetComponent<VirtualButton>());
                            }
                            break;
                        case InputEventType.Changed:
                            if (changedEventDict.ContainsKey(virtualRealPair.VirtualButton.GetComponent<VirtualButton>().Name))
                            {
                                changedEventDict[virtualRealPair.VirtualButton.GetComponent<VirtualButton>().Name](virtualRealPair.VirtualButton.GetComponent<VirtualButton>());
                            }
                            break;
                        case InputEventType.Update:
                            if (updateEventDict.ContainsKey(virtualRealPair.VirtualButton.GetComponent<VirtualButton>().Name))
                            {
                                updateEventDict[virtualRealPair.VirtualButton.GetComponent<VirtualButton>().Name](virtualRealPair.VirtualButton.GetComponent<VirtualButton>());
                            }
                            break;
                    }
                }
            }
        }
        
    }
  
        
    /// <summary>
    /// Method to add a custom event function to be fired when a key meets the InputEventType
    /// </summary>
    /// <param name="_virtualKey">The key to have the event connected to</param>
    /// <param name="_eventType">The type of event, when it is fired</param>
    /// <param name="_function">The code to be fired, has input data passed as first argument</param>
    public void AddEvent(String _virtualKey, InputEventType _eventType, VirtualFunction _function)
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
        return _inh;
    }
    
    /// <summary>
    /// Returns the current map
    /// </summary>
    /// <returns>KeyMap</returns>
    public GameObject Map()
    {
        return _currentMap;
    }
    
    /// <summary>
    /// Sets the current map
    /// </summary>
    /// <param name="newMap">The map to be made current</param>
    public void SetMap(GameObject newMap)
    {
        if (newMap != null)
        {
            _currentMap = newMap;
        }
        else
        {
            Debug.LogWarning("Map does not exist! Aborting Attempt");
        }
    }
}
