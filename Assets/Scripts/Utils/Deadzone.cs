using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deadzone : MonoBehaviour
{
    [SerializeField] private CameraController _cameraController;
     
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.CompareTag(ObjectTags.Character)) 
        {
            collision.gameObject.GetComponent<Character>().InstantDeath();
            _cameraController.enabled = false;
        }
    }

}
