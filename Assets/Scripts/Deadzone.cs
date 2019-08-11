using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deadzone : MonoBehaviour
{
    CameraController cam;

    public void Awake()
    {
        cam = FindObjectOfType<CameraController>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Unit unit = collision.GetComponent<Unit>(); //проверяем, есть ли на этом коллайдере обьекти типа Юнит
        Character character = collision.GetComponent<Character>();
        if (character) 
        {
            
            character.InstantDeath();
            cam.enabled = false;
            
        }
    }

}
