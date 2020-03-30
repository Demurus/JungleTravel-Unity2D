using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bottle : Bullet
{
    private float _speed = 15.0F;
    private float _destroyTimer = 0.2F;
    
    public AudioClip BottleCollapse;
   

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    protected override void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, _speed * Time.deltaTime);
     }

    protected override void Collapse(float destroyTimer)
    {
        base.Collapse(_destroyTimer);
        PlaySound(BottleCollapse);
        _speed = 0F;
    }

    protected override void OnTriggerEnter2D(Collider2D collider)
    {
        Unit character = collider.GetComponent<Character>();

        if (character)
        {
            character.ReceiveDamage(); 
            Collapse(_destroyTimer);
        }
        else
        {
            PlaySound(BottleCollapse);
            base.OnTriggerEnter2D(collider);
        }

    }
   
}
