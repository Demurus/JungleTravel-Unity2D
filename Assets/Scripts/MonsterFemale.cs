using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterFemale : Monster
{
    public AudioClip smallZombieFemaleDeath;
    
   

    protected override AudioClip GetMonsterDeathAudio()
    {
        return smallZombieFemaleDeath;
    }
    
    

}


