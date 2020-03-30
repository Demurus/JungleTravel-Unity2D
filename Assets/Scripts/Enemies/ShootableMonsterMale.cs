using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootableMonsterMale : Monster
{
    private Rigidbody2D _rigidBody;

    protected Bottle bottle;
    protected float fireRate = 2F;
    protected float nextFire = 0.0F;

    public AudioClip AlcoZombieHurt;
    public AudioClip AlcoZombieMaleDeath;
    
    protected override AudioClip GetMonsterDeathAudio()
    {
        return AlcoZombieMaleDeath;
    }
    protected override void Awake()
    {
        base.Awake();
        bottle = Resources.Load<Bottle>(ObjectTags.Bottle);
        _rigidBody = GetComponent<Rigidbody2D>();
       
    }
    protected override void Update()
    {
         base.Move();
         Shoot();
    }


    protected override void OnTriggerEnter2D(Collider2D collider)
   {
        
    }
    protected override void Shoot()
    {
        if (monsterIsDead)
        {
            return;
        }

        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Vector2 monsterVelocity = _rigidBody.velocity;
            Vector3 position = transform.position; position.y += 1.0F;
            Bottle newBottle = Instantiate(bottle, position, bottle.transform.rotation) as Bottle;
            newBottle.Direction = newBottle.transform.right * monsterVelocity.x;
            Animation(AnimationTags.Shoot);
         }
        
    }
   

}
