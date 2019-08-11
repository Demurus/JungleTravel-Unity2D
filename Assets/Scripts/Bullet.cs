using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
   
    public Vector3 direction;
    protected Animator animator;
    public Vector3 Direction { set { direction = value; } }

   
    public float volBulletLowRange = 0.5F;
    public float volBulletHighRange = 1.0F;

    virtual protected void Collapse()
    {
        
        animator.SetBool("Collapse", true);
        Destroy(gameObject, 0.2F);
    } 
    virtual protected void OnTriggerEnter2D(Collider2D collider)
    {
        
        if (collider.tag == Obstacle.Tag)
        {
            Collapse();
        }
    }

   

    protected virtual void Update() { }
   

    private void Start()
    {
        Destroy(gameObject, 3.4F);
    }


   



}
