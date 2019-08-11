using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootableMonsterfemale : ShootableMonsterMale
{
    
    public AudioClip AlcoZombieFemaleDeath;
    

    protected override  AudioClip GetAudio()
    {
        return AlcoZombieFemaleDeath;
    }
  
}
