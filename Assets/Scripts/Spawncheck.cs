using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawncheck : MonoBehaviour
{
    protected Vector3 playerSpawnPlace;
    public bool isPassed;
    private void Awake()
    {
        playerSpawnPlace= new Vector3(transform.position.x, transform.position.y, transform.position.z);
        //Debug.Log(playerSpawnPlace);
    }
    private void Update()
    {
        //Debug.Log(isPassed);
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        //Character character = FindObjectOfType<Character>();
        
        Unit character = collider.GetComponent<Character>();
        if (character) isPassed = true;

    }
}
