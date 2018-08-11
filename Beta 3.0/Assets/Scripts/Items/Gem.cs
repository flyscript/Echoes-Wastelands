/*
  Gems
  
  Will Chapman
  09/08/2018
  
  
*/

//Using
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Page behaviour
/// </summary>
public class Gem : MonoBehaviour
{

    [SerializeField]
    private int _name;

    [SerializeField]
    private string _newPageDescription;
    
    [SerializeField]
    private GameObject _gem4;
  
    /// <summary>
    /// The name of the gem
    /// </summary>
    public int Name
    {
        get { return _name; }
        set { /*readonly*/ Debug.LogWarning("Cannot set Name. Property is readonly. Try making a new item."); }
    }
    
    /// <summary>
    /// The description of the page when Gem 4 is collected
    /// </summary>
    public string Description
    {
        get { return _newPageDescription; }
        set { /*readonly*/ Debug.LogWarning("Cannot set Name. Property is readonly. Try making a new item."); }
    }
    
    /// <summary>
    /// A pointer to Gem 4
    /// </summary>
    public GameObject Gem4
    {
        get { return _gem4; }
        set { /*readonly*/ Debug.LogWarning("Cannot set Gems. Property is readonly. Try making a new item."); }
    }
    
    /// <summary>
    /// Overrides the update method of the Item, so that Unity's monobehaviour runs this
    /// </summary>
    public void Update()
    {
        //Rotates every update
        transform.Rotate(new Vector3(0, 50, 0) * Time.deltaTime);
    }

    /// <summary>
    /// Overriding method to describe behaviour on collectable pickup
    /// </summary>
    /// <param name="player">Player object</param>
    public void OnPickup(GameObject player)
    {
        
        if (player != null && player.GetComponent<PlayerMain>()!=null)
        {
            Debug.Log("Player picked up gem");
            //TODO: add some sort of ding sound effect
            
            GameData.controller.data.GetFromJournal(SceneManager.GetActiveScene().name).Gems[_name - 1] = true;
            
            //If all three blue gems have been collected
            if (GameData.controller.data.GetFromJournal(SceneManager.GetActiveScene().name).Gems[0] &&
                GameData.controller.data.GetFromJournal(SceneManager.GetActiveScene().name).Gems[1] &&
                GameData.controller.data.GetFromJournal(SceneManager.GetActiveScene().name).Gems[2] &&
                !GameData.controller.data.GetFromJournal(SceneManager.GetActiveScene().name).Gems[3]
            )
            {
                Gem4.SetActive(true);
            }
            //If this is the 4th gem
            else if (_name == 4)
            {
                GameData.controller.data.GetFromJournal(SceneManager.GetActiveScene().name).Description = _newPageDescription;
            }
            
            gameObject.SetActive(false);
        }
    }
    
}