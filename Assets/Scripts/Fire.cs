using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Unit unit = collision.GetComponent<Unit>(); 
        if (unit && unit is Character) 
        {
            unit.ReceiveDamage();
        }
    }
    
}

