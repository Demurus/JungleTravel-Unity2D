using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;


public class Boss : Monster
{
    [SerializeField] private BossHealthBar _bossHealthBar;
    private Animator _animator;
    private GameManager _gameManager;
    private AudioManager _audioManager;
    private Rigidbody2D _rigidBody;
    private Character _character;
    private int _throwStrenght;
    private Transform _target;
    private float _bossSpeed = 8.0F;
    private float _bossLives = 10.0F;
    private int _activeFunctionState = 0;
    private float _timerToRun=3.0f;
    private float _timerToShoot=3.0f;
    private float _timerToIdle=3.2f;
    private bool _isDead = false;
    private bool _isVulnerable = false;
    private bool _facingRight = false;

    protected Barrel barrel;
    protected float fireRate = 0.8F;
    protected float nextFire = 0.0F;

    public GameObject deathExplosion;
   
      
    public float Lives
    {
        get
        {
            return _bossLives;
        }
        set
        {
            _bossLives = value;
            if (_bossLives <= 0)
            {
                BossIsDead();
            }
        }
    }

    private BossState _bossState
    {
        get { return (BossState)_animator.GetInteger(AnimationTags.State); }
        set { _animator.SetInteger(AnimationTags.State, (int)value); }
    }
    protected override void Awake()
    {
        _animator = GetComponent<Animator>();
        _rigidBody = GetComponent<Rigidbody2D>();
        _gameManager = GameObject.FindGameObjectWithTag(ControllerTags.GameManager).GetComponent<GameManager>();
        _audioManager = GameObject.FindGameObjectWithTag(ControllerTags.AudioManager).GetComponent<AudioManager>();
        _character= GameObject.FindGameObjectWithTag(ObjectTags.Character).GetComponent<Character>();
        barrel = Resources.Load<Barrel>(ObjectTags.Barrel);
    }

    protected override void Start()
    {
        base.Start();
        _target = _character.transform;
        _bossHealthBar.Refresh(Lives);
    }

    private void BossDamage()
    {
        Lives--;
    }

    private void ActiveFunction()
    {
        if (_isDead)
        {
            return;
        }

        switch (_activeFunctionState)
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
        _isVulnerable = false;
        _bossSpeed = 0F;
        if (_target.position.x > transform.position.x && !_facingRight) //if the target is to the right of enemy and the enemy is not facing right
        {
            Flip();
        }
        if (_target.position.x < transform.position.x && _facingRight)
        {
            Flip();
        }
        _bossState = BossState.JumpAndShoot;
        Invoke("ShootDelay",1.0F);
    }

    private void ShootDelay()
    {
        _throwStrenght = Random.Range(0, 8);
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Vector3 position = transform.position;
            position.y += 2.0F;
            position.x -= 2.0F;
            Barrel newBarrel = Instantiate(barrel, position, barrel.transform.rotation) as Barrel;
            newBarrel.Direction = newBarrel.transform.right * (_target.position.x - transform.position.x) + transform.up * _throwStrenght;
        }
    }

     protected override void Update()
    {
       ActiveFunction();
    }

    protected override void Flip()
    {
        base.Flip();
        _facingRight = !_facingRight;
    }

    private void Idle()
    {
        _bossState = BossState.Idle;
        _isVulnerable = true;
    }
    private void Run()
    {
        _bossSpeed = 8.0F;
        _isVulnerable = false;
        _bossState = BossState.Run;
        Vector2 monsterVelocity = _rigidBody.velocity;
        monsterVelocity.x = monsterTransform.right.x * -_bossSpeed;
        _rigidBody.velocity = monsterVelocity;
    }

    private void BossIsDead()
    {
        _isDead = true;
        _audioManager.Stop(SoundTags.BossMusic);
        _audioManager.Play(SoundTags.BossDeathSequence);
        _animator.SetTrigger(AnimationTags.Dead);
        GetComponent<Collider2D>().enabled = false;
        StartCoroutine(WaitForExplosion());
    }
    private void DeathExplosion()
    {
        Instantiate(deathExplosion, transform.position, transform.rotation);
        _character.StopThePlayerMovement();
    }
    protected override void OnCollisionEnter2D(Collision2D collision)
    {

        if (_isDead)
        {
            return;
        }
            if (collision.gameObject.GetComponent<Character>())
             {
                _character.ReceiveDamage();
                 if (!_facingRight)
                    {
                      _character.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 10.0F, ForceMode2D.Impulse);
                     }
                 else _character.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 10.0F, ForceMode2D.Impulse);
        }
           if (collision.gameObject.CompareTag(ObjectTags.Obstacle))
             {
                 Flip();
             }

    }

    protected override void OnTriggerEnter2D(Collider2D collider)
    {
    
        if (collider.CompareTag(ObjectTags.CoconutBullet) && _isVulnerable == true)
        {
            BossDamage();
            _bossHealthBar.Refresh(Lives);
            _animator.SetTrigger(AnimationTags.Hit);
        }
    }
   
   
   
    IEnumerator CounterToRun()
    {
        yield return new WaitForSeconds(_timerToRun);
        _activeFunctionState = 1;

    }
 
    IEnumerator CounterToShoot()
    {
        yield return new WaitForSeconds(_timerToShoot);
        _activeFunctionState = 2;
    }

    IEnumerator CounterToIdle()
    {
        yield return new WaitForSeconds(_timerToIdle);
        _activeFunctionState = 0;
    }
    
    IEnumerator WaitForExplosion()
    {
        yield return new WaitForSeconds(3.3F);
        DeathExplosion();
        _audioManager.Play(SoundTags.BossExplosion);
        yield return new WaitForSeconds(1.0F);
        _audioManager.Play(SoundTags.LevelPassed);
        yield return new WaitForSeconds(3.0F);
        _gameManager.LevelPassed();
    }
    
}
public enum BossState
{
    Idle,
    Run,
    JumpAndShoot
}
