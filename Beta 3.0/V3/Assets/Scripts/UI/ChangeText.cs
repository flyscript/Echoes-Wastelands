using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChangeText : MonoBehaviour
{
    [SerializeField]
    private Text instructions;

    private PlayerMain playerInventory;
    private Item test;

    private void Start()
    {
        playerInventory = GameObject.Find("Character").GetComponent<PlayerMain>();
    }
    
    private void Update()
    {
        test = playerInventory.GetFromInventory("Crystal");

        
        if (test == null)
        {
            Debug.Log(test);
        }
        else
        {
            instructions.text = test.Description;
        }
        
        
        
//        if (playerInventory != null && test != null && playerInventory.IsInInventory("Coin"))
//        {
//            
//        }
//        else
//        {
//            //instructions.text = "Mate, You ain't collect shit!";
//        }
    }

}
