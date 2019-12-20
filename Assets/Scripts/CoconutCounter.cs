using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoconutCounter : MonoBehaviour
{
   
    private Character character;
    public Text coconutsText;
    
    void Start()
    {
        SetText();
    }

   
    public void Refresh()
    {
        SetText();
    }

    void SetText()
    {
        character = FindObjectOfType<Character>();
        coconutsText.text = "x" + character.CoconutsAmount.ToString();
     }
}
