using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public static readonly string TagObstacle = "Obstacle";
    public static readonly string TagFloor = "Floor";
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Unit unit = collision.collider.GetComponent<Unit>(); 
        if(unit && unit is Character) 
        {
            
            unit.ReceiveDamage();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Unit unit = collision.GetComponent<Unit>();
        if (unit && unit is Character) 
        {
           
            unit.ReceiveDamage();
        }
    }
   
}
