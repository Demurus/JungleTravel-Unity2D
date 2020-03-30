using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class Unit : MonoBehaviour
{
   
    public AudioClip[] charPunchArray = new AudioClip[3];
    protected AudioSource charPunchsource;

    protected virtual void Flip() { }
    protected virtual void Awake() {}
    protected virtual void Start() {}
    protected virtual void Update() { }
    protected virtual void FixedUpdate() { }
    public virtual void ReceiveDamage()
    {
        Die();
    }
    
    public virtual void Die()
    {
        Destroy(gameObject);
    }

}