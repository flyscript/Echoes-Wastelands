/*
  Items
  
  Will Chapman
  Callum Yates
  06/07/2018
  
  Collectables inherits from Items and describes, currently, a coin
  
*/

//Using
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Describes behaviour for collectables
/// </summary>
public class Collectables : Item
{
    [SerializeField] 
    private Text displayScore;
    
    
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
        string score;
        
        if (player != null && player.GetComponent<PlayerMain>()!=null)
        {
            Debug.Log("Player picked up collectable");
            //TODO: add some sort of ding sound effect
            player.GetComponent<PlayerMain>().Points += 10;
            score = player.GetComponent<PlayerMain>().Points.ToString();
            displayScore.text = score;
            gameObject.SetActive(false);
        }
    }
}