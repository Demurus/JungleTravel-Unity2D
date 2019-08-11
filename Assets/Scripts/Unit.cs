﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class Unit : MonoBehaviour
{
    public float volLowRange = 0.5F;
    public float volHighRange = 1.0F;
    
    public AudioClip charPunch1;
    public AudioClip charPunch2;
    public AudioClip charPunch3;
    
    public AudioClip[] charPunchArray = new AudioClip[3];
    protected AudioSource charPunchsource;
    


    protected virtual void Awake() {}
    protected virtual void Start() {}
    protected virtual void Update() { }
    public virtual void ReceiveDamage()
    {
        Die();
    }
    
    public virtual void Die()
    {
        Destroy(gameObject);
    }

    //public enum UnitState
    //{
    //    Idle,
    //    Dead
    //}

}