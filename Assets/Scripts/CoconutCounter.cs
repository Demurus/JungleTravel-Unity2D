using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoconutCounter : MonoBehaviour
{
    //public Transform coconuts;
    private Character character;
    public Text coconutsText;
    // Start is called before the first frame update
    void Start()
    {
        SetText();
    }

    // Update is called once per frame
    public void Refresh()
    {
        SetText();
    }

    void SetText()
    {
        character = FindObjectOfType<Character>();
        coconutsText.text = "x" + character.CoconutsAmount.ToString();
       // coconutsText.GetComponent<Text>().text= character.CoconutsAmount.ToString();
    }
}
