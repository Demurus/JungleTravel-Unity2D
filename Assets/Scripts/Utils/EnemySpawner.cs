using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Transform[] Spawnpoints;
    public GameObject[] EnemyPrefabs;
    public Transform Parent;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(ObjectTags.Character))
        {
            for(int i=0; i<=Spawnpoints.Length-1; i++)
            {

                GameObject enemyPrefab = Instantiate(EnemyPrefabs[Random.Range(0, EnemyPrefabs.Length)], Spawnpoints[i].position, Spawnpoints[i].rotation);
                enemyPrefab.transform.SetParent(Parent);
            }
           
        }
        //this.GetComponent<Collider2D>().enabled = false;
    }
}
