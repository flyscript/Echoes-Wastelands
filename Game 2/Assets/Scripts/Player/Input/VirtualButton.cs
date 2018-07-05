/*
  Virtual Button
  
  Will Chapman
  15/06/2018
  27/06/2018
    
*/

//Using
using UnityEngine;

//Holds all stats for a virtual button
//Inherits from Input Data, sets keycode to keyCode.None as not likely to be used anywhere else
class VirtualButton : InputData
{
    /*//////////////////////////////
    ///   Initialise Variables   ///
    //////////////////////////////*/
  
    /// <summary>
    /// The name of the virtual button
    /// </summary>
    private string name;
  
    
    
    /*//////////////////////////////
    ///       Class Methods      ///
    //////////////////////////////*/
    
    /// <summary>
    /// Creates new virtual button
    /// </summary>
    /// <param name="_name">Name of virtual button</param>
    public VirtualButton(string _name) : base (KeyCode.None, InputType.Virtual)
    {
        name = _name;
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

  
    /*Called by KeyMapManager, should not really be invoked by user
    public void override Update(float[] _newPosition)
    {
      //Update position delta
      positionDelta[0] = _newPosition[0] - Position[0];
      positionDelta[1] = _newPosition[1] - Position[1];
      //Update position
      position = _newPosition;
      //Update Position Time
      if (positionDelta[0] == 0 && positionDelta[1] == 0)
      {
        positionTime += Time.deltaTime;
      }
      //Update Time Tracked
      timeTracked += Time.deltaTime;
      //Update LastUpdate
      lastUpdate = Time.time;
    }*/
  
}