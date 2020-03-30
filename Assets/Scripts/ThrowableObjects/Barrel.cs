using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : Bullet
{
    private int _speed;
    private bool _isCollapsed;
    private Rigidbody2D _rigidbody;
    private float _detroyTimer=0.9f;
    private AudioSource _audio;

    public AudioClip[] BarrelCollapseArray;
    public GameObject BarrelExplosion;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    protected override void Update()
    {
        if (_isCollapsed)
        {
            return;
        }
          _speed = Random.Range(10, 14);
          transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, _speed * Time.deltaTime);
    }

    protected override void Collapse(float destroyTimer)
    {
        _isCollapsed = true;
        base.Collapse(_detroyTimer);
        _rigidbody.velocity = Vector3.zero;
        Instantiate(BarrelExplosion, transform.position, transform.rotation);
        PlaySound(BarrelCollapseArray[Random.Range(0, 3)]);
    }

    protected override void OnTriggerEnter2D(Collider2D collider)
    {
        Unit character = collider.GetComponent<Character>();
       if (_isCollapsed)
        {
            return;
        }

        if (character)
        {
            Debug.Log(collider);
            character.ReceiveDamage();
            Collapse(_detroyTimer);

        }
        else base.OnTriggerEnter2D(collider);

    }
}
