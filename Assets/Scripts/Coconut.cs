using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coconut : Bullet
{
    private float speed = 10.0F;
    //private Vector3 direction;
    //public Vector3 Direction { set { direction = value; } }
    private SpriteRenderer sprite;
    private GameObject parent;
    //private Animator animator;
    new private Rigidbody2D rigidbody;

    public AudioClip coconutColapse;
    private AudioSource source;
    public GameObject Parent
    {
        set { parent = value; }
    }

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
        //float vol = Random.Range(volBulletLowRange, volBulletHighRange);
        source.PlayOneShot(coconutColapse);
        speed = 0F;
        //animator.SetBool("Collapse", true);
       // Destroy(gameObject, 0.2F);
    }


    protected override void OnTriggerEnter2D(Collider2D collider)
    {
        Monster monster = collider.GetComponent<Monster>();
        Unit unit = collider.GetComponent<Unit>();
       
        if (unit != null && unit.gameObject != parent)
        {
            Collapse();
        }
        else
        {
            base.OnTriggerEnter2D(collider);
        }
        
    }
   


}
