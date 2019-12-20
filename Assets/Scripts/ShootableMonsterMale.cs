using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootableMonsterMale : Monster
{
    protected Bottle bottle;
    protected float fireRate = 2F;
    protected float nextFire = 0.0F;
    public AudioClip alcoZombieHurt;
    public AudioClip AlcoZombieMaleDeath;
    
    protected override AudioClip GetMonsterDeathAudio()
    {
        return AlcoZombieMaleDeath;
    }
    protected override void Awake()
    {
        base.Awake();
        bottle = Resources.Load<Bottle>("Bottle");
       
    }
    protected override void Update()
    {

       // if (IsDead()) return;
       // else
        //{
            base.Move();
            Shoot();
       // }
    }

   // protected override void OnCollisionExit2D(Collision2D collision)
   // {
        
  //  }

    protected override void OnTriggerEnter2D(Collider2D collider)
   {
        
    }
    protected override void Shoot()
    {
        if (monsterIsDead)
        {
            return;
        }

        Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Vector2 monsterVelocity = rigidbody.velocity;
            Vector3 position = transform.position; position.y += 1.0F;
            Bottle newBottle = Instantiate(bottle, position, bottle.transform.rotation) as Bottle;
            newBottle.Direction = newBottle.transform.right * monsterVelocity.x;
            StartCoroutine(shootableMonsterMaleIdler());
         }
        
    }
    IEnumerator shootableMonsterMaleIdler()
    {
        State = MonsterState.Shoot;
        yield return new WaitForSeconds(0.2F);
        State = MonsterState.ReturnToIdle;
        
    }

    

}
