using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    protected Vector3 direction;
    protected Animator animator;
    public AudioSource Audio;

  
    public Vector3 Direction
    {
        set
        {
            direction = value;
        }
    }

    private void Awake()
    {
        Audio = GetComponent<AudioSource>();
    }
    virtual protected void Collapse(float destroyTimer)
    {
        animator.SetBool(AnimationTags.Collapse, true);
        StartCoroutine(DestroyBulletTimer(destroyTimer));
    } 

    virtual protected void PlaySound(AudioClip audioClip)
    {
        Audio.PlayOneShot(audioClip);
    }
    virtual protected void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == ObjectTags.Obstacle||collider.tag==ObjectTags.Floor)
        {
            Collapse(0.2f);
        }
    }

    protected virtual void Update() { }
   
    private void Start()
    {
        StartCoroutine(DestroyBulletTimer(3.5F));
    }

    IEnumerator DestroyBulletTimer(float timer)
    {
        yield return new WaitForSeconds(timer);
        Destroy(gameObject);
    }
}
