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
    //public AudioClip characterHurt1, characterHurt2, characterHurt3;
    public AudioClip[] charHurt = new AudioClip[3];
    //public AudioClip characterJump1, characterJump2, characterJump3, characterJump4, characterJump5, characterJump6, characterJump7, characterJump8;
    public AudioClip[] charJumpArray = new AudioClip[8];
    private AudioSource source;

    //private float volLowRange = 0.5F;
   // private float volHighRange = 1.0F;

    protected override void Awake() 
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        coconut = Resources.Load<Coconut>("Bullet"); // загружаем префаб буллет из папки Resources (в иерархии)
        livesBar = FindObjectOfType<LivesBar>();
        coconutCounter = FindObjectOfType<CoconutCounter>();
        source = GetComponent<AudioSource>();

        //charHurt[0] = characterHurt1;
        //charHurt[1] = characterHurt2;
        //charHurt[2] = characterHurt3;
        //charJumpArray[0] = characterJump1;
        //charJumpArray[1] = characterJump2;
        //charJumpArray[2] = characterJump3;
        //charJumpArray[3] = characterJump4;
        //charJumpArray[4] = characterJump5;
        //charJumpArray[5] = characterJump6;
        //charJumpArray[6] = characterJump7;
        //charJumpArray[7] = characterJump8;


    }

    protected override void Update()
    {
       if (!isDead)
        {
            if (isGrounded) State = CharState.Idle;
            if (Input.GetButton("Horizontal")) Run();
            if (isGrounded && Input.GetButtonDown("Jump")) Jump();
            if (Input.GetButton("Fire1") && Time.time > nextFire)
            {
                nextFire = Time.time + fireRate;
                if (coconutsAmount > 0) Shoot();
            }
            
        }
        ///else animator.SetBool("Dead", true);

    }

    private void FixedUpdate()
    {
        if(!isDead) CheckGround();
     }

    

    private void Run()
    {
        characterDirection = transform.right * Input.GetAxis("Horizontal");
        
        //Vector3 direction = transform.right * Input.GetAxis("Horizontal");
        transform.position = Vector3.MoveTowards(transform.position, transform.position + characterDirection, speed * Time.deltaTime);
        sprite.flipX = characterDirection.x < 0; // перевернуть обьект тогда, когда направление по х меньше 0. направление вперед(вправ)-это 1, напр. назад (влево) это -1
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
        if (!isGrounded) State = CharState.Jump;
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
      Debug.Log(coconutsAmount);
    }

    public void InstantDeath()
    {
       // Lives = Lives - 5;
        rigidbody.velocity = Vector3.zero;
        //source.PlayOneShot(characterDeath);
        //Destroy(gameObject);
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
            //StartCoroutine(BackToIdle());
            animator.SetTrigger("Hit");
             State = CharState.BackToIdle;


            Debug.Log(lives);
        }

         if (Lives <= 0 && !isDead)
        {
         
            //isDead = true;
            //animator.SetBool("Dead", true);
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
    //IEnumerator BackToIdle()
    //{
    //    animator.SetTrigger("Hit");
        
        //State = CharState.Hit;
        //yield return new WaitForSeconds(0.1F);
       // State = CharState.Idle;

   // }

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