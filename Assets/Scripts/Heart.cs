using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    private Animator animator;
    private SpriteRenderer sprite;
    public AudioClip heartTake;
    private AudioSource source;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        source = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        Character character = collider.GetComponent<Character>();
        if(character != null) 
        {
            if (character.Lives < 5)
            {
                character.Lives++;
                source.PlayOneShot(heartTake);
                animator.SetBool("Puff", true);
                Destroy(gameObject, 0.6F);

            }
        }
    }
}
