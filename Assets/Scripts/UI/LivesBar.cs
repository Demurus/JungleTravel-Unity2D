using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivesBar : MonoBehaviour
{
    private Transform[] _heartsArray = new Transform[5];
    private void Awake()
    {    
        for (int i=0; i< _heartsArray.Length; i++)
        {
            _heartsArray[i] = transform.GetChild(i);
        }
    }

    public void Refresh(int lives)
    {
        for (int i=0;i< _heartsArray.Length;i++)
        {
            if (i < lives)
            {
                _heartsArray[i].gameObject.SetActive(true);
            }
            else
            {
                _heartsArray[i].gameObject.SetActive(false);
            }
        }
    }
}
 