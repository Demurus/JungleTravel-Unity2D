using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bottle : Bullet
{
    private float speed = 15.0F;
   // private SpriteRenderer sprite;
    //new private Rigidbody2D rigidbody;
    public AudioClip bottleCollapse;
    private AudioSource source;

    private void Awake()
    {
        //rigidbody = GetComponent<Rigidbody2D>();
       // sprite = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponent<Animator>();
        source = GetComponent<AudioSource>();
    }

    protected override void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);
     }

    protected override void Collapse()
    {
        base.Collapse();
        source.PlayOneShot(bottleCollapse);
        speed = 0F;
     }

    protected override void OnTriggerEnter2D(Collider2D collider)
    {
        Unit character = collider.GetComponent<Character>();
     
        if (character!=null)
        {
           character.ReceiveDamage();
            Collapse();
        }
        else base.OnTriggerEnter2D(collider);

    }
   
}
