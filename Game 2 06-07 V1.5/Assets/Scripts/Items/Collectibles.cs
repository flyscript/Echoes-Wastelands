/*
  Items
  
  Will Chapman
  Callum Yates
  06/07/2018
  
  Collectables inherits from Items and describes, currently, a coin
  
*/

//Using
using UnityEngine;

public class Collectibles : Item
{
    protected Collectibles(string name, string description)
        : base("Collectible " + name, description, ItemType.Collectables)
    {
    }
    
    
    public override void Update()
    {
        transform.Rotate(new Vector3(0, 50, 0) * Time.deltaTime);
    }

    public override void OnPickup(PlayerMain playerMain)
    {
        //TODO: add some sort of ding sound effect
        playerMain.Points += 10;
        playerMain.AddToInventory(this);
		gameObject.SetActive(false);
        
    }
}