using System;
  using System.Collections;
  using System.Collections.Generic;
  using UnityEngine;
  
  /* Authors
      Sandy Saran
      Callum Yates
      
      Will Chapman
   */
  public class Destructable : MonoBehaviour
  {
  
      // Update is called once per frame
      public void Destroy()
      {
         //TODO: Other effects triggered here?
          
         Destroy(gameObject);
      }
      
   }