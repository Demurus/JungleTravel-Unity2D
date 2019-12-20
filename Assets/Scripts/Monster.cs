using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : Unit
{
    
    protected float speed = 2.0F;
    protected int lives = 2;
   
    protected Animator animator;
    private Animator charAnimator;
    private SpriteRenderer sprite;
   
    protected bool monsterStop = false;
    protected Transform monsterTransform;
    protected float monsterWidth;
    public LayerMask enemyMask;
    public AudioClip smallZombieHurt;
    public AudioClip smallZombieMaleDeath;
    private AudioSource source;
    protected bool monsterIsDead = false;


    protected virtual AudioClip GetZombieHurtAudio()
    {
        return smallZombieHurt;
    }
     protected virtual AudioClip GetMonsterDeathAudio()
    {
        return smallZombieMaleDeath;
    }
    protected void DestroyMonster()
    {
       
        Destroy(gameObject);
    }
    protected override void Start()
    {
        monsterTransform = transform;
        monsterWidth = GetComponentInChildren<SpriteRenderer>().bounds.extents.x;
    }
    public override void ReceiveDamage()
    {
        monsterIsDead = true;
        State = MonsterState.Dead;
        monsterStop = true;
        source.PlayOneShot(GetMonsterDeathAudio());
        
        GetComponent<Collider2D>().enabled = false;
        Invoke("DestroyMonster", 10.0F);
    }
    protected virtual MonsterState State
    {
        get { return (MonsterState)animator.GetInteger("State"); }
        set { animator.SetInteger("State", (int)value); }
    }
    protected virtual void Shoot() { }
    protected bool IsDead()
    {
        return State == MonsterState.Dead;
    }

    protected override void Update()
    {
         Move();
    }

    protected override void Awake()
    {
      
        animator = GetComponent<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        source = GetComponent<AudioSource>();
        charPunchsource = GetComponent<AudioSource>();
       

    }

    protected override void Flip()
    {
        Vector3 currentRotation = monsterTransform.eulerAngles;
        currentRotation.y += 180;
        monsterTransform.eulerAngles = currentRotation;
    }
    protected void Move()
    {
        if (monsterIsDead)
        {
            return;
        }
        
            Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
            Vector2 lineCastPosition = monsterTransform.position - monsterTransform.right * monsterWidth;

            bool isGrounded = Physics2D.Linecast(lineCastPosition, lineCastPosition + Vector2.down, enemyMask);

            if (!isGrounded)
            {
                Flip();
            }

            Vector2 monsterVelocity = rigidbody.velocity;
            monsterVelocity.x = monsterTransform.right.x * -speed;

            rigidbody.velocity = monsterVelocity;

            if (monsterStop)
            {
                speed = 0F;
            }

    }

     protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        
        Character character = collision.gameObject.GetComponent<Character>();
        Unit unit = GetComponent<Monster>();

        if (IsDead())
        {
            return;
        }
            if (character)
            {
            
              foreach (ContactPoint2D point in collision.contacts)
                {
                    
                    if (point.normal.y == -1.0F)
                    {
                        unit.ReceiveDamage();
                        int punchRandom = Random.Range(0, 2);
                        charPunchsource.PlayOneShot(charPunchArray[punchRandom]);
                        character.rigidbody.AddForce(transform.up * 15.0F, ForceMode2D.Impulse);
                        break;
                            }
                    else
                    {
                        State = MonsterState.Attack;
                        character.ReceiveDamage();
                        break;
                    }

            }

        }
        if (collision.collider.tag == "Obstacle" || collision.collider.tag=="Monster")
        {
            Flip();
        }

    }

    protected virtual void OnCollisionExit2D(Collision2D collision)
    {
        Character character = collision.collider.GetComponent<Character>();
        if (character)
        {
            if (!IsDead())
            {
                State = MonsterState.Idle;
            }
           

        }
    }
   
    protected virtual void OnTriggerEnter2D(Collider2D collider)
    {
        Character character = collider.gameObject.GetComponent<Character>();
        Unit unit = GetComponent<Monster>();
        Coconut coconut = collider.GetComponent<Coconut>();
        if (coconut)
        {
            lives--;
            if (lives <= 0) ReceiveDamage(); 
            else
            {
                State = MonsterState.Hit;
                source.PlayOneShot(smallZombieHurt,1.5F);
                StartCoroutine(takeHitPause());
            }

        }

    }

    IEnumerator takeHitPause()
    {
        yield return new WaitForSeconds(0.1F);
        State = MonsterState.Idle;
    }
    public enum MonsterState
    {
        Idle,
        Dead,
        Attack,
        Hit,
        Shoot,
        ReturnToIdle,
        Run
    }
}
