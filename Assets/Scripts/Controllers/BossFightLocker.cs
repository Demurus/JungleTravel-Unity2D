using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFightLocker : MonoBehaviour
{

    [SerializeField] private Canvas _canvas;
    [SerializeField] private AudioManager _audioManager;
    
    void Start()
    {
        this.GetComponent<BoxCollider2D>().enabled = false;
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.GetComponent<Character>())
        {
            StartCoroutine(BlockerTimer());
            _audioManager.Stop(SoundTags.LevelMusic);
            _audioManager.Stop(SoundTags.Sfx);
            _audioManager.Play(SoundTags.BossMusic);
            StartCoroutine(CanvasBlinker());
        }
    }
    IEnumerator CanvasBlinker()
    {
        for (int i = 0; i < 5; i++)
        {
         _canvas.enabled = true;
         yield return new WaitForSeconds(0.5F);
         _canvas.enabled = false;
         yield return new WaitForSeconds(0.5F);
        }
    }
    IEnumerator BlockerTimer()
    {
        yield return new WaitForSeconds(0.5F);
        this.GetComponent<BoxCollider2D>().enabled = true;
    }
    
}
