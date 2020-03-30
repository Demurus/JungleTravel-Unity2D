using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : Unit {


    private readonly float _speed = 6.0F;
    private int _lives = 5;
    private int _maxlives=5;
    private int _coconutsAmount = 5;
    private LivesBar _livesBar;
    private CoconutCounter _coconutCounter;
    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private Bullet _coconutBullet;
    private GameManager _gameManager;
    private readonly float _jumpForce = 17.0F;
    private bool _isGrounded = false;
    private Vector3 _characterDirection;
    private float _fireRate = 0.5F;
    private float _nextFire = 0.0F;
    private AudioSource _source;

    public AudioClip CoconutThrow;
    public AudioClip CharacterDeath;
    public AudioClip[] CharHurtArray = new AudioClip[3];
    public AudioClip[] CharJumpArray = new AudioClip[8];
    public bool IsDead = false;
    public bool PlayerCanMove = true;

    public int Lives
    {
        get
        {
            return _lives;
        }
        set
        {
           _lives = Mathf.Clamp(value, 0, _maxlives);
           _livesBar.Refresh(_lives);
            if (value <= 0 && !IsDead)
            {
                Death();
            }

        }
    }

    public int CoconutsAmount
    {
        get
        {
            return _coconutsAmount;
        }
        set
        {
            _coconutsAmount = value;
            _coconutCounter.Refresh(_coconutsAmount);
        }
    }

    public CharState State
    {
        get
        {
            return (CharState)_animator.GetInteger(AnimationTags.State);
        }
        set
        {
            _animator.SetInteger(AnimationTags.State, (int)value);
        }
    }
    
    protected override void Awake() 
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _source = GetComponent<AudioSource>();
        _coconutBullet = Resources.Load<Coconut>(ObjectTags.Bullet); 
        _livesBar = GetComponentInChildren<LivesBar>();
        _coconutCounter = GetComponentInChildren<CoconutCounter>();
    }

    protected override void Start()
    {
        _coconutCounter.Refresh(_coconutsAmount);
    }
    protected override void FixedUpdate()
    {
        if (IsDead || !PlayerCanMove)
        {
            return;
        }
            CheckGround();
            if (_isGrounded) State = CharState.Idle;
            if (Input.GetButton(ControllerTags.Horizontal)) Run();
            if (_isGrounded && Input.GetButtonDown(ControllerTags.Jump)) Jump();
            if (Input.GetButton(ControllerTags.FireLeftMouseButton) && Time.time > _nextFire)
            {
                _nextFire = Time.time + _fireRate;
                Shoot();
            }
    }

   

    private void Run()
    {
        _characterDirection = transform.right * Input.GetAxis(ControllerTags.Horizontal);
        transform.position = Vector3.MoveTowards(transform.position, transform.position + _characterDirection, _speed * Time.deltaTime);
        _spriteRenderer.flipX = _characterDirection.x < 0; // flip object when its X direction less than 0. Forward(right) direction is 1, backwards(left) dir. is -1 
        if (_isGrounded)
        {
            State = CharState.Run;
        }
    }

    private void Jump()
    {
        _rigidbody.AddForce(transform.up * _jumpForce, ForceMode2D.Impulse);
        int charJumpIndex = UnityEngine.Random.Range(0, 7);
        _source.PlayOneShot(CharJumpArray[charJumpIndex]);
    }

    private void CheckGround()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.9f);
        _isGrounded = colliders.Length > 1;
        if (!_isGrounded)
        {
            State = CharState.Jump;
        }
    }

    private void Shoot()
    {
        if (_coconutsAmount <= 0)
        {
            return;
        }
            _coconutsAmount--;
            _source.PlayOneShot(CoconutThrow, 2.0F);
            Vector3 position = transform.position; position.y += 1.0F;
            Coconut newCoconut = Instantiate(_coconutBullet, position, _coconutBullet.transform.rotation) as Coconut;
            newCoconut.Parent = gameObject;
            newCoconut.Direction = newCoconut.transform.right * _characterDirection.x;
            _animator.SetTrigger(AnimationTags.Shoot);
            _coconutCounter.Refresh(_coconutsAmount);
    }

    public void InstantDeath()
    {
        _rigidbody.velocity = Vector3.zero;
        Death();
    }
    public void StopThePlayerMovement()
    {
        PlayerCanMove = false;
    }
    public override void ReceiveDamage()
    {

        if (IsDead)
        {
            return;
        }
        else
        {
            Lives--;
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.AddForce(transform.up * 9.0F, ForceMode2D.Impulse);
            int hurtRandom = UnityEngine.Random.Range(0, 2);
            _source.PlayOneShot(CharHurtArray[hurtRandom]);
            _animator.SetTrigger(AnimationTags.Hit); 
        }

    }

    private void Death()
    {
        IsDead = true;
        _animator.SetTrigger(AnimationTags.Dead);
        _source.PlayOneShot(CharacterDeath);
        FindObjectOfType<GameManager>().EndGame();
     }

    public bool LivesTaker()
    {
        if (Lives >= _maxlives)
        {
            return false;
        }
        else
        {
            Lives++;
            return true;
        }
    }
   public void CoconutsTaker()
    {
         CoconutsAmount++;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(ObjectTags.Spikes))
        {
            ReceiveDamage();
        }
    }
}

public enum CharState
{
    Idle,
    Run,
    Jump
}