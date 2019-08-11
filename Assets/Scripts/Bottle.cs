using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bottle : Bullet
{
    private float speed = 15.0F;
    private SpriteRenderer sprite;
    //private Animator animator;
    new private Rigidbody2D rigidbody;
    public AudioClip bottleCollapse;
    private AudioSource source;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
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
        // float vol = Random.Range(volBulletLowRange, volBulletHighRange);
        source.PlayOneShot(bottleCollapse);
        speed = 0F;
        //animator.SetBool("Collapse", true);
        //Destroy(gameObject, 0.2F);
    }

    protected override void OnTriggerEnter2D(Collider2D collider)
    {
        Unit character = collider.GetComponent<Character>();
        
        //Monster monster = collider.GetComponent<Monster>();
       // Unit unit = collider.GetComponent<Unit>();
        // animator.SetBool("Collapse", true);
        if (character!=null)
        {
           character.ReceiveDamage();
            Collapse();
        }
        else base.OnTriggerEnter2D(collider);

    }
    //IEnumerator BackToIdle()
   // {
   //     Character character = GetComponent<Character>();
    //    character.State = CharState.Hit;
   //     yield return new WaitForSeconds(0.2F);
   //     character.State = CharState.BackToIdle;
//
   // }
}
