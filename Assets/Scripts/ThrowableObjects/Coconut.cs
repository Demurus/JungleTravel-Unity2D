using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coconut : Bullet
{
    private float _speed = 10.0F;
    private GameObject _parent;
    private SpriteRenderer _spriteRenderer;
    private float _destroyTimer=0.3F;
    
    public GameObject MuzzleFlash;
    public AudioClip CoconutColapse;
    public GameObject Parent
    {
        set { _parent = value; }
    }

    private void Awake()
    {
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    protected override void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, _speed * Time.deltaTime);
    }

    protected override void Collapse(float destroyTimer)
    {
        base.Collapse(_destroyTimer);
        _spriteRenderer.enabled = false;
        PlaySound(CoconutColapse);
        Instantiate(MuzzleFlash, transform.position, transform.rotation);
        _speed = 0F;
    }


    protected override void OnTriggerEnter2D(Collider2D collider)
    {
        Unit unit = collider.GetComponent<Unit>();
       
        if (unit != null && unit.gameObject != _parent)
        {
            Collapse(_destroyTimer);
        }
        else
        {
            base.OnTriggerEnter2D(collider);
        }
        
    }

}
