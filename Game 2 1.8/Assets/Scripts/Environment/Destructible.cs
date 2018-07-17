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
        if (_ccs.Melee && _hasPlayerHitDestructible) 
        {
            gameObject.SetActive(false);
            _ccs.Melee = false;
            _hasPlayerHitDestructible = false;
        }
    }
    
    private void OnCollisionEnter2D()
    {
        _hasPlayerHitDestructible = true;
    }
    private void OnCollisionExit2D()
    {
        _hasPlayerHitDestructible = false;
    }
}