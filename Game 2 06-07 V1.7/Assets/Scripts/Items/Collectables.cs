/*
  Items
  
  Will Chapman
  Callum Yates
  06/07/2018
  
  Collectables inherits from Items and describes, currently, a coin
  
*/

//Using
using UnityEngine;

/// <summary>
/// Describes behaviour for collectables
/// </summary>
public class Collectables : Item
{
    /// <summary>
    /// Defines collectable when instantiated
    /// </summary>
    /// <param name="name">Name of the collectable</param>
    /// <param name="description">Description of the collectable</param>
    protected Collectables(string name, string description)
        : base("Collectible " + name, description, ItemType.Collectables)
    {
    }
    
    
    /// <summary>
    /// Overrides the update method of the Item, so that Unity's monobehaviour runs this
    /// </summary>
    public override void Update()
    {
        //Rotates every update
        transform.Rotate(new Vector3(0, 50, 0) * Time.deltaTime);
    }

    /// <summary>
    /// Overriding method to describe behaviour on collectable pickup
    /// </summary>
    /// <param name="player">Player object</param>
    public override void OnPickup(GameObject player)
    {
        if (player != null && player.GetComponent<PlayerMain>()!=null)
        {
            Debug.Log("Player picked up collectable");
            //TODO: add some sort of ding sound effect
            player.GetComponent<PlayerMain>().Points += 10;
            player.GetComponent<PlayerMain>().AddToInventory(this);
            gameObject.SetActive(false);
        }
    }
}