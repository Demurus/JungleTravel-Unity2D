using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoconutCounter : MonoBehaviour
{
   
    public Text coconutsText;
    
    public void Refresh(int coconutsAmount)
    {
        coconutsText.text = "x" + coconutsAmount.ToString();
    }

    
}
