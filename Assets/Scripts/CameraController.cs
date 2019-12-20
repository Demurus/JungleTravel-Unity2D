using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
   public float speed = 1.3F;
    public float posZ = -10.0F;
    public float posYcoef = 1.5F;
    
    public float ort = 7.0f;
   
    
    public Transform target;
    

    private void Awake()
    {
        //target = Resources.Load<Character>("Character").transform;
        //if (!target) target = FindObjectOfType<Character>().transform;
        //Character character = FindObjectOfType<Character>();
       
    }

    private void Start()
    {
        StartCoroutine("CameraFinder");
        //if (!target) target = FindObjectOfType<Character>().transform;
    }
    private void Update()
    {
        // if (!target) target = FindObjectOfType<Character>().transform;
        Camera.main.orthographicSize = ort;
        Vector3 position = target.position;
        position.z = posZ;
        position.y = posYcoef+(position.y);
        transform.position = Vector3.Lerp(transform.position,position, (speed*1.5F) * Time.deltaTime);
     }

    IEnumerator CameraFinder()
    {
        yield return new WaitForSeconds(0.1F);
        if (!target) target = FindObjectOfType<Character>().transform;
    }
    
}
