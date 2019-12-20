using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : Bullet
{
    private int speed;
    private SpriteRenderer sprite;
    private bool isCollapsed;
    new private Rigidbody2D rigidbody;
    public AudioClip [] barrelCollapseArray=new AudioClip[3];
    private AudioSource source;
    public GameObject barrelExplosion;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponent<Animator>();
        source = GetComponent<AudioSource>();
    }

    protected override void Update()
    {
        if (!isCollapsed)
        {
            speed = Random.Range(10, 14);
            transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);
        }
    }

    protected override void Collapse()
    {
        isCollapsed = true;
        base.Collapse();
        Instantiate(barrelExplosion, transform.position, transform.rotation);
        int barrelCollapseIndex = Random.Range(0, 2);
        source.PlayOneShot(barrelCollapseArray[barrelCollapseIndex]);
        speed = 0;
    }

    protected override void OnTriggerEnter2D(Collider2D collider)
    {
        Unit character = collider.GetComponent<Character>();
       if (!isCollapsed)
        {
            if (character != null)
            {
                character.ReceiveDamage();
                Collapse();

            }
            else base.OnTriggerEnter2D(collider);
        }
            
    }
}
