using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
   
    private Character _character;
    private Transform _camera;
  
    public GameObject checkpoint1;
    public GameObject checkpoint2;
    public static int PassedAmount = 0;


    private void Awake()
    {
        _character = Resources.Load<Character>(ObjectTags.Character);
        _camera = Camera.main.transform;
    }
    void Start()
    {
        Debug.Log(PassedAmount);
        switch (PassedAmount)
        {
            case 0:
                Instantiate(_character, transform.position, Quaternion.identity);
                break;
            case 1:
                Instantiate(_character, checkpoint1.transform.position, Quaternion.identity);
                _camera.transform.position = checkpoint1.transform.position;
                break;
            case 2:
                Instantiate(_character, checkpoint2.transform.position, Quaternion.identity);
                _camera.transform.position = checkpoint2.transform.position;
                break;
            default:
                Instantiate(_character, checkpoint2.transform.position, Quaternion.identity);
                _camera.transform.position = checkpoint2.transform.position;
                break;

        }

    }

}
