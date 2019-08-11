using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private float speed = 2.0F;
    
    public Transform target;
    

    private void Awake()
    {
        if (!target) target = FindObjectOfType<Character>().transform;
        //Character character = FindObjectOfType<Character>();
       

    }

    private void Update()
    {
        Vector3 position = target.position;
        position.z = -10.0F;
        position.y = 1.4F+(position.y);
        transform.position = Vector3.Lerp(transform.position,position, (speed*1.5F) * Time.deltaTime);
     }

    
}
