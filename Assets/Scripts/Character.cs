using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : Unit {

    
    private readonly float speed = 6.0F;
   
    private int lives = 5;
    public int Lives
    {
        get { return lives; }
        set
        {
            if (value <=5) lives = value;
            livesBar.Refresh();
        }
    }

    private int coconutsAmount = 5;
    public int CoconutsAmount
    {
        get { return coconutsAmount; }
        set {
            coconutsAmount = value;
            coconutCounter.Refresh();
        }
    }


    private LivesBar livesBar;
    private CoconutCounter coconutCounter;
    private int checklives=5;
    
    private readonly float jumpForce = 17.0F;
    private bool isGrounded = false;
    private Bullet coconut;
    Vector3 characterDirection;
    public float fireRate = 0.5F;
    private float nextFire = 0.0F;
    public bool isDead = false;
    public bool playerCanMove = true;
    
    
    public CharState State
    {
        get { return (CharState)animator.GetInteger("State"); }
        set { animator.SetInteger("State", (int)value); }
    }
    
    new public Rigidbody2D rigidbody;
    private Animator animator;
    private SpriteRenderer sprite;

    public AudioClip coconutThrow;
    public AudioClip characterDeath;
    public AudioClip[] charHurt = new AudioClip[3];
    public AudioClip[] charJumpArray = new AudioClip[8];
    private AudioSource source;

   
    protected override void Awake() 
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        coconut = Resources.Load<Coconut>("Bullet"); 
        livesBar = FindObjectOfType<LivesBar>();
        coconutCounter = FindObjectOfType<CoconutCounter>();
        source = GetComponent<AudioSource>();
        

    }
    
    protected override void Update()
    {
        if (isDead || !playerCanMove)
        {
            return;
        }
        


            if (isGrounded) State = CharState.Idle;
            if (Input.GetButton("Horizontal")) Run();
            if (isGrounded && Input.GetButtonDown("Jump")) Jump();
            if (Input.GetButton("Fire1") && Time.time > nextFire)
            {
                nextFire = Time.time + fireRate;
                if (coconutsAmount > 0) Shoot();
            }
            
       

    }

    private void FixedUpdate()
    {
        if (isDead) return;
        else CheckGround();
     }

    

    private void Run()
    {
        characterDirection = transform.right * Input.GetAxis("Horizontal");
        transform.position = Vector3.MoveTowards(transform.position, transform.position + characterDirection, speed * Time.deltaTime);
        sprite.flipX = characterDirection.x < 0; // flip object when its X direction less than 0. Forward(right) direction is 1, backwards(left) dir. is -1 
        if (isGrounded) State = CharState.Run;
    }

    private void Jump()
    {
        
        rigidbody.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
        int charJumpIndex = Random.Range(0, 7);
        source.PlayOneShot(charJumpArray[charJumpIndex]);
    }

    private void CheckGround()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.9f);
        isGrounded = colliders.Length > 1;
        if (!isGrounded && State!=CharState.Hit) State = CharState.Jump;
    }

    private void Shoot()
    {
        coconutsAmount--;
        float vol = Random.Range(volLowRange, volHighRange);
        source.PlayOneShot(coconutThrow, 2.0F);
        Vector3 position = transform.position; position.y += 1.0F;
      Coconut newCoconut = Instantiate(coconut, position, coconut.transform.rotation) as Coconut;
        newCoconut.Parent = gameObject;
        newCoconut.Direction = newCoconut.transform.right * characterDirection.x;
      State = CharState.Shoot;
      coconutCounter.Refresh();
    }

    public void InstantDeath()
    {
        rigidbody.velocity = Vector3.zero;
        Death();
    }
    public override void ReceiveDamage()
    {
        if (!isDead)
        {
            Lives--;
            rigidbody.velocity = Vector3.zero;
            rigidbody.AddForce(transform.up * 9.0F, ForceMode2D.Impulse);
            int hurtRandom = Random.Range(0, 2);
            source.PlayOneShot(charHurt[hurtRandom]);
            animator.SetTrigger("Hit");
             State = CharState.BackToIdle;
        }

         if (Lives <= 0 && !isDead)
        {
               Death();
        };
        
        
    }

    private void Death()
    {
        isDead = true;
        State = CharState.Dead;
        source.PlayOneShot(characterDeath);
        FindObjectOfType<Character>().tag = "Obstacle";
        FindObjectOfType<GameManager>().EndGame();
     }
   
}

public enum CharState
{
    Idle,
    Run,
    Jump,
    Shoot,
    Hit,
    Dead,
    BackToIdle
}