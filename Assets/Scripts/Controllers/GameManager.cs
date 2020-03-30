using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private AudioManager _audioManager;
    private bool _gameHasEnded = false;
    private float _restartDelay = 2F;

    public GameObject CurrentCheckPoint;
    public GameObject LevelPassedUI;

    private void Start()
    {
        _audioManager.Play(SoundTags.LevelMusic);
        _audioManager.Play(SoundTags.Sfx);
    }
    public void LevelPassed()
    {
        LevelPassedUI.SetActive(true);
    }

    public void EndGame()
    {
        if (_gameHasEnded == false)
        {
           _gameHasEnded = true;
            Invoke("Restart", _restartDelay);
        }
    }
    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
   
}
