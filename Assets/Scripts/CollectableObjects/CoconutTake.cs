using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoconutTake : MonoBehaviour
{
    private AudioSource _audio;

    public AudioClip CoconutTaken;
    private void Awake()
    {
        _audio = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        Character character = collider.GetComponent<Character>();
        if (character)
        {
            character.CoconutsTaker();
            _audio.PlayOneShot(CoconutTaken,4.0F);
            Destroy(gameObject,0.12F);
        }
    }
}
