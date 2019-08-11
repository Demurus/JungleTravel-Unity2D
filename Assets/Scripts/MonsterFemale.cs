using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterFemale : Monster
{
    public AudioClip smallZombieFemaleDeath;
    
    protected string monsterName = "Female";

    protected override AudioClip GetAudio()
    {
        return smallZombieFemaleDeath;
    }
    //protected override void Start()
    //{
    //    monsterTransform = transform;
    //    monsterWidth = GetComponentInChildren<SpriteRenderer>().bounds.extents.x;
    //}
    

}


