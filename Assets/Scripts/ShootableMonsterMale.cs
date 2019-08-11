using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootableMonsterMale : Unit
{
    protected float speed = 2.0F;
   
    protected Animator animator;
    private SpriteRenderer sprite;
    protected bool monsterStop = false;
    protected Bottle bottle;
    protected Transform monsterTransform;
    protected float monsterWidth;
    public LayerMask enemyMask;
    protected float fireRate = 2F;
    protected float nextFire = 0.0F;

    public AudioClip AlcoZombieMaleDeath;
    private AudioSource source;
    protected virtual AudioClip GetAudio()
    {
        return AlcoZombieMaleDeath;
    }
    protected override void Awake()
    {
        
        animator = GetComponent<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        bottle = Resources.Load<Bottle>("Bottle");
        source = GetComponent<AudioSource>();
        charPunchsource = GetComponent<AudioSource>();
       /* charPunchArray[0] = charPunch1;
        charPunchArray[1] = charPunch2;
        charPunchArray[2] = charPunch3;*/
    }
    protected override void Start()
    {
        monsterTransform = transform;
        monsterWidth = GetComponentInChildren<SpriteRenderer>().bounds.extents.x;
    }

    protected bool IsDead()
    {
        return State == ShootableMonsterState.Dead;
    }
    protected override void Update()
    {
       
        if (!IsDead())
        {
            Move();
            Shoot();
        }
    }

    protected void Move()
    {
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
    protected ShootableMonsterState State
    {
        get { return (ShootableMonsterState)animator.GetInteger("State"); }
        set { animator.SetInteger("State", (int)value); }
    }
    protected void OnCollisionEnter2D(Collision2D collision)
    {

        Character character = collision.gameObject.GetComponent<Character>();
        Unit unit = GetComponent<ShootableMonsterMale>();


        if (character)
        {
            if (State != ShootableMonsterState.Dead)
            {
                foreach (ContactPoint2D point in collision.contacts)
                {
                    Debug.Log(point.normal);
                    //Debug.DrawLine(point.point, point.point + point.normal, Color.red, 10);
                    if (point.normal.y == -1.0F)
                    {
                        unit.ReceiveDamage();
                        int punchRandom = Random.Range(0, 2);
                        charPunchsource.PlayOneShot(charPunchArray[punchRandom]);
                        source.PlayOneShot(GetAudio());
                        character.rigidbody.AddForce(transform.up * 15.0F, ForceMode2D.Impulse);
                        break;
                    }
                    else
                    {
                        //State = ShootableMonsterMaleState.Attack;
                        character.ReceiveDamage();
                        break;
                    }

                }

            }

        }
        if (collision.collider.tag == "Obstacle" || collision.collider.tag == "Monster")
        {

            if (State != ShootableMonsterState.Dead) Flip();
        }

    }

    protected void Flip()
    {
        Vector3 currentRotation = monsterTransform.eulerAngles;
        currentRotation.y += 180;
        monsterTransform.eulerAngles = currentRotation;
    }

    protected void Shoot()
    {
        Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
        if (Time.time > nextFire)
        {
            //animator.SetInteger("State", 2);
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
        State = ShootableMonsterState.Shoot;
        yield return new WaitForSeconds(0.2F);
        State = ShootableMonsterState.ReturnToIdle;
        
    }

    protected void DestroyMonster()
    {
        Destroy(gameObject);
    }
    public override void ReceiveDamage()
    {
        State = ShootableMonsterState.Dead;
        monsterStop = true;
        Debug.Log(State);
        GetComponent<Collider2D>().enabled = false;
        Invoke("DestroyMonster", 10.0F);
    }

    
   
    public enum ShootableMonsterState 
    {
        Idle,
        Dead,
        Shoot,
        ReturnToIdle,
        Attack
    }

}
