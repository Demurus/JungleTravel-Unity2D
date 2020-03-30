using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    private Boss _boss;
    private Vector3 _place;
    private void Awake()
    {
        _boss = Resources.Load<Boss>(ObjectTags.Boss);
    }
  
    private void OnTriggerEnter2D(Collider2D collider)
    {
        Unit character = collider.GetComponent<Character>();
        _place = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        if(character)
        {
            Instantiate(_boss,_place,Quaternion.identity);
        }

    }
}
