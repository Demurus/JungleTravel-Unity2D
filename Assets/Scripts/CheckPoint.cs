using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : PlayerSpawner
{
    
    void Start()
    {
        //spawner = FindObjectOfType<PlayerSpawner>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Unit character = collider.GetComponent<Character>();
        if (character)
        {
            
            passedAmount++;
            Debug.Log(passedAmount);
            //spawner.currentCheckPoint = gameObject;
        }

    }

}
