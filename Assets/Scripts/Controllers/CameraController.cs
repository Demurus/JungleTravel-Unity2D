using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    
    [SerializeField] private float _speed = 1.3F;
    [SerializeField] private float _positionZ = -10.0F;
    [SerializeField] private float _positionYCoefficient = 1.5F;
    [SerializeField] private float _orthographicCoefficient = 7.0f;
   
    public Transform Target;
    

    private void Start()
    {
        StartCoroutine(CameraTargetFinder());
       
    }
    private void Update()
    {
        if (!Target)
        {
            return;
        }
             Camera.main.orthographicSize = _orthographicCoefficient;
             Vector3 position = Target.position;
             position.z = _positionZ;
             position.y = _positionYCoefficient+(position.y);
             transform.position = Vector3.Lerp(transform.position,position, (_speed*1.5F) * Time.deltaTime);
     }

    IEnumerator CameraTargetFinder()
    {
        yield return new WaitForSeconds(0.1F);
        Target = GameObject.FindGameObjectWithTag(ObjectTags.Character).GetComponent<Character>().transform;
    }
    
}
