using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : Unit
{
    private AudioSource _audio;
    private Character _character;
    private Animator _animator;

    protected Rigidbody2D rigidBody;
    protected float speed = 2.0F;
    protected int lives = 2;
    protected bool monsterIsDead = false;
    protected Transform monsterTransform;
    protected float monsterWidth;

    public LayerMask EnemyMask;
    public AudioClip SmallZombieHurt;
    public AudioClip SmallZombieMaleDeath;

    
    protected override void Awake()
    {
        _animator = GetComponent<Animator>();
        _character = GameObject.FindGameObjectWithTag(ObjectTags.Character).GetComponent<Character>();
        _audio = GetComponent<AudioSource>();
        charPunchsource = GetComponent<AudioSource>();
        rigidBody = GetComponent<Rigidbody2D>();
    }
    protected override void Start()
    {
        monsterTransform = transform;
        monsterWidth = GetComponentInChildren<SpriteRenderer>().bounds.extents.x;
    }

    protected void Animation(string animationName)
    {
        _animator.SetTrigger(animationName);
    }

    protected virtual AudioClip GetZombieHurtAudio()
    {
        return SmallZombieHurt;
    }
    protected virtual AudioClip GetMonsterDeathAudio()
    {
        return SmallZombieMaleDeath;
    }
    public override void ReceiveDamage()
    {
        monsterIsDead = true;
        Animation(AnimationTags.Dead);
        speed = 0.0f;
        _audio.PlayOneShot(GetMonsterDeathAudio());
        
        GetComponent<Collider2D>().enabled = false;
        StartCoroutine(WaitToDestroy());
    }
   
    protected virtual void Shoot() { }
   
    protected override void Update()
    {
         Move();
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
        
            Vector2 lineCastPosition = monsterTransform.position - monsterTransform.right * monsterWidth;
            bool isGrounded = Physics2D.Linecast(lineCastPosition, lineCastPosition + Vector2.down, EnemyMask);

            if (!isGrounded)
            {
                Flip();
            }

            Vector2 monsterVelocity = rigidBody.velocity;
            monsterVelocity.x = monsterTransform.right.x * -speed;
            rigidBody.velocity = monsterVelocity;
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (monsterIsDead)
        {
            return;
        }
        if (collision.gameObject.CompareTag(ObjectTags.Character))
        {

            foreach (ContactPoint2D point in collision.contacts)
            {

                if (point.normal.y == -1.0F)
                {
                    ReceiveDamage();
                    charPunchsource.PlayOneShot(charPunchArray[Random.Range(0, 2)]);
                    _character.GetComponent<Rigidbody2D>().AddForce(transform.up * 15.0F, ForceMode2D.Impulse);
                    break;
                }
                else
                {
                    Animation(AnimationTags.Attack);
                    _character.ReceiveDamage();
                    break;
                }

            }

        }

       
        if (collision.gameObject.CompareTag(ObjectTags.Obstacle)|| collision.gameObject.CompareTag(ObjectTags.Monster))
        {
            Flip();
        }

    }

    protected virtual void OnTriggerEnter2D(Collider2D collider)
    {
      
        if (collider.gameObject.CompareTag(ObjectTags.CoconutBullet))
        {
            lives--;
            if (lives <= 0)
            {
                ReceiveDamage();
            }
            else
            {
                Animation(AnimationTags.Hit);
                _audio.PlayOneShot(SmallZombieHurt, 1.5F);
            }

        }

    }

    IEnumerator WaitToDestroy()
    {
        yield return new WaitForSeconds(10.0F);
        Destroy(gameObject);
    }
   
   
}
