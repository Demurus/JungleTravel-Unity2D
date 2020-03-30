using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    private Animator _animator;
    private AudioSource _audio;

    public AudioClip HeartTake;


    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _audio = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        Character character = collider.GetComponent<Character>();
        if(character) 
        {
            if (character.LivesTaker() == true)
            {
                _audio.PlayOneShot(HeartTake);
                _animator.SetTrigger(AnimationTags.HeartPuff);
                Destroy(gameObject, 0.6F);
            }
            
           
        }
    }
}
