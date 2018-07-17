using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeText : MonoBehaviour
{
    [SerializeField]
    private Text instructions;

    public void TextChange()
    {
        instructions.text = "HOLY SHIT, WHY THE HELL DO I HAVE TO GO COLLECT STUPID SUPPLIES FOR MY SHITTY ASS TRIBE? RISKING MY ASS, THIS IS DUMB AF!";
    }
}
