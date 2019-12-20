using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public static int passedAmount=0;
    private Character character;
    Vector3 place;
    public GameObject checkpoint1;
    public GameObject checkpoint2;
    private Transform cam;
    
    private void Awake()
    {
        character = Resources.Load<Character>("Character");
        cam = Camera.main.transform;
    }
    void Start()
    {
        Debug.Log(passedAmount);
        switch (passedAmount)
        {
            case 0:
                Instantiate(character, transform.position, Quaternion.identity);
                //cam.transform.position = transform.position;
                break;
            case 1:
                Instantiate(character, checkpoint1.transform.position, Quaternion.identity);
                cam.transform.position = checkpoint1.transform.position;
                break;
            case 2:
                Instantiate(character, checkpoint2.transform.position, Quaternion.identity);
                cam.transform.position = checkpoint2.transform.position;
                break;
            default:
                Instantiate(character, checkpoint2.transform.position, Quaternion.identity);
                cam.transform.position = checkpoint2.transform.position;
                break;

        }

    }

}
