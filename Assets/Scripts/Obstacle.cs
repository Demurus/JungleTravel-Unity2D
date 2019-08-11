using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public static readonly string Tag = "Obstacle";
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Unit unit = collision.collider.GetComponent<Unit>(); //проверяем, есть ли на этом коллайдере обьекти типа Юнит
        if(unit && unit is Character) // проверка если это Юнит, и в то же время наш Character
        {
            //foreach(ContactPoint2D point in collider.GetContacts)

            unit.ReceiveDamage();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Unit unit = collision.GetComponent<Unit>(); //проверяем, есть ли на этом коллайдере обьекти типа Юнит
        if (unit && unit is Character) // проверка если это Юнит, и в то же время наш Character
        {
            //foreach(ContactPoint2D point in collider.GetContacts)

            unit.ReceiveDamage();
        }
    }
   
}
