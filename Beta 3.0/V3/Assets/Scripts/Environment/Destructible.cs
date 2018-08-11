using System;
  using System.Collections;
  using System.Collections.Generic;
  using UnityEngine;
  
  /* Authors
      Sandy Saran
      Callum Yates
   */
  public class Destructible : MonoBehaviour
  {
      private Rigidbody2D _rigidBody;
      private CharacterControllerScript _ccs;
      private bool _hasPlayerHitDestructible;
  
      // Use this for initialization
      void Start()
      {
          _rigidBody = GetComponent<Rigidbody2D>();
          _ccs = FindObjectOfType<CharacterControllerScript>();
      }
  
      // Update is called once per frame
      void Update()
      {
         // Debug.Log(_ccs.Melee +" "+ _hasPlayerHitDestructible);
          if (_ccs.Melee && _hasPlayerHitDestructible) 
          {
              gameObject.SetActive(false);
              _ccs.Melee = false;
              _hasPlayerHitDestructible = false;
            
          }
      }
      // yh boiiiiiii
      private void OnCollisionEnter2D(Collision2D other)
      {
          if (other.gameObject.CompareTag("Player"))
          {
              _hasPlayerHitDestructible = true;
          }
      }
      
      private void OnCollisionExit2D(Collision2D other)
       {
           if (other.gameObject.CompareTag("Player"))
           {
               _hasPlayerHitDestructible = false;
           }
       }
   }