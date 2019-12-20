using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private Boss boss;
    int bossID; 
    Vector3 place;
    private void Awake()
    {
        boss = Resources.Load<Boss>("Boss");
    }
  
    private void OnTriggerEnter2D(Collider2D collider)
    {
       
        Unit character = collider.GetComponent<Character>();
        place = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        if(character)
        {
            Instantiate(boss,place,Quaternion.identity);
        }

    }
}
