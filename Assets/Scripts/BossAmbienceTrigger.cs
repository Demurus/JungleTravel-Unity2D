using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAmbienceTrigger : MonoBehaviour
{
    
    //private SpriteRenderer sprite;
    private Canvas canvas;
  //  private Canvas canvas1;

    void Start()
    {
        BossAmbienceTrigger trigger = GetComponent<BossAmbienceTrigger>();
        trigger.GetComponent<BoxCollider2D>().enabled = false;
        FindObjectOfType<audioManager>().Play("levelMusic");
        FindObjectOfType<audioManager>().Play("sfx");
    }

    private void Awake()
    {
        canvas = GetComponentInChildren<Canvas>();
        //canvas1 = GetComponent<Canvas>();
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        Unit character = collider.GetComponent<Character>();
       // sprite = GetComponentInChildren<SpriteRenderer>();
        if (character)
        {
            StartCoroutine(BlockerTimer());
            FindObjectOfType<audioManager>().Stop("levelMusic");
            FindObjectOfType<audioManager>().Stop("sfx");
            FindObjectOfType<audioManager>().Play("bossMusic");
            StartCoroutine(CanvasBlinker());
        }
    }
    IEnumerator CanvasBlinker()
    {
        for (int i = 0; i < 5; i++)
        {
         canvas.enabled = true;
         yield return new WaitForSeconds(0.5F);
         canvas.enabled = false;
         yield return new WaitForSeconds(0.5F);
        }
    }
    IEnumerator BlockerTimer()
    {
        BossAmbienceTrigger trigger = GetComponent<BossAmbienceTrigger>();
        yield return new WaitForSeconds(0.5F);
        trigger.GetComponent<BoxCollider2D>().enabled = true;

    }
    
}
