using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coconut_take : MonoBehaviour
{

    public AudioClip coconutTake;
    private AudioSource source;
    private void Awake()
    {
       source = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        Character character = collider.GetComponent<Character>();
        CoconutCounter coconutCounter = FindObjectOfType<CoconutCounter>();
        
        if (character)
        {
            character.CoconutsAmount++;
            source.PlayOneShot(coconutTake,4.0F);
            Destroy(gameObject,0.12F);
            coconutCounter.Refresh();
        }
    }
}
