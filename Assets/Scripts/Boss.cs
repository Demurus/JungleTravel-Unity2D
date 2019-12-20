using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;


public class Boss : Monster
{
    private float bossSpeed = 8.0F;
    private float bossLives = 10.0F;
    protected Barrel barrel;
    protected float fireRate = 0.8F;
    protected float nextFire = 0.0F;
    private int activeFunctionState=0;
    private bool isDead = false;
    private bool isVulnerable = false;
    bool facingRight = false;
    public GameObject deathExplosion;
    public BossGameManager gameManager;
      
    public float Lives
    {
        get { return bossLives; }
        set { bossLives = value; }
    }

    private Animator charAnimator;
    private SpriteRenderer sprite;
    private BossHealthBar bossHealthBar;
    int throwStrenght;
    private Transform target;

    protected override void Awake()
    {
        animator = GetComponent<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        barrel = Resources.Load<Barrel>("Barrel");
        bossHealthBar = FindObjectOfType<BossHealthBar>();

    }

    protected override void Start()
    {
        base.Start();
        target = FindObjectOfType<Character>().transform;
        
    }
    
    private void BossIsDead()
    {
        isDead = true;
        FindObjectOfType<audioManager>().Stop("bossMusic");
        FindObjectOfType<audioManager>().Play("bossDeathSequence");
        State = MonsterState.Dead;
        GetComponent<Collider2D>().enabled = false;
        StartCoroutine(waitForExplosion());
     }

    private void BossDamage()
    {
        Lives--;
    }

    private void ActiveFunction()
    {
        if (isDead)
        { return; }

        switch (activeFunctionState)
            {
                case 0:
                    Idle();
                    StartCoroutine(CounterToRun());
                    break;
                case 1:
                    Run();
                    StartCoroutine(CounterToShoot());
                    break;
                case 2:
                    Shoot();
                    StartCoroutine(CounterToIdle());
                    break;
             }
 
    }

      protected override void Shoot()
    {
        isVulnerable = false;
        bossSpeed = 0F;
        if (target.position.x > transform.position.x && !facingRight) //if the target is to the right of enemy and the enemy is not facing right
        {
            Flip();
        }
        if (target.position.x < transform.position.x && facingRight)
        {
            Flip();
        }
        State = MonsterState.Shoot;
        Invoke("ShootDelay",1.0F);
    }

    private void ShootDelay()
    {
        throwStrenght = Random.Range(0, 8);
        Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Vector3 position = transform.position;
            position.y += 2.0F;
            position.x -= 2.0F;
            Barrel newBarrel = Instantiate(barrel, position, barrel.transform.rotation) as Barrel;
            newBarrel.Direction = newBarrel.transform.right * (target.position.x - transform.position.x) + transform.up * throwStrenght;
        }
    }

     protected override void Update()
    {
       ActiveFunction();
    }

   
    protected override void Flip()
    {
        base.Flip();
        facingRight = !facingRight;
    }

    private void Idle()
    {
        State = MonsterState.Idle;
        isVulnerable = true;
    }
    private void Run()
    {
        bossSpeed = 8.0F;
        isVulnerable = false;
        Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
        State = MonsterState.Run;
        Vector2 monsterVelocity = rigidbody.velocity;
        monsterVelocity.x = monsterTransform.right.x * -bossSpeed;
        rigidbody.velocity = monsterVelocity;
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        Character character = collision.gameObject.GetComponent<Character>();
        Unit unit = GetComponent<Boss>();

        if (isDead)
        {
            return;
        }
            if (character)
             {
                character.ReceiveDamage();
                 if (!facingRight)
                 {
                     character.rigidbody.AddForce(Vector2.left * 10.0F, ForceMode2D.Impulse);
                 }
                 else character.rigidbody.AddForce(Vector2.right * 10.0F, ForceMode2D.Impulse);
            }
                if (collision.collider.tag == "Obstacle")
                {
                    Flip();
                }

    }

    protected override void OnTriggerEnter2D(Collider2D collider)
    {
      
        Coconut coconut = collider.GetComponent<Coconut>();
        if (coconut&&isVulnerable==true)
        {
            bossLives -= 1.0F;
            bossHealthBar.Refresh();

            if (bossLives <= 0)
            {
                BossIsDead();
            }
            else
            {
                animator.SetBool("Hurt", true);
                StartCoroutine(takeHitPause());
            }

        }

    }

    IEnumerator CounterToRun()
    {
        yield return new WaitForSeconds(3.0F);
        activeFunctionState = 1;

    }
 
    IEnumerator CounterToShoot()
    {
        yield return new WaitForSeconds(3.0F);
        activeFunctionState = 2;
    }

    IEnumerator CounterToIdle()
    {
        yield return new WaitForSeconds(3.2F);
        activeFunctionState = 0;
    }
    IEnumerator takeHitPause()
    {
        yield return new WaitForSeconds(0.4F);
         animator.SetBool("Hurt", false);
       
    }
    IEnumerator waitForExplosion()
    {
        yield return new WaitForSeconds(3.3F);
        Instantiate(deathExplosion, GetComponent<Rigidbody2D>().transform.position, GetComponent<Rigidbody2D>().transform.rotation);
        Character overridePlayerMovement = FindObjectOfType<Character>();
        overridePlayerMovement.playerCanMove = false;
        FindObjectOfType<audioManager>().Play("bossExplosion");
        yield return new WaitForSeconds(1.0F);
        FindObjectOfType<audioManager>().Play("levelPassed");
        yield return new WaitForSeconds(3.0F);
        gameManager.LevelPassed();
    }
    
}

