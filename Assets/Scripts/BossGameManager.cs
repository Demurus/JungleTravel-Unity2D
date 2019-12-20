using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossGameManager : MonoBehaviour
{
    public GameObject levelPassedUI;
    public void LevelPassed()
    {
        levelPassedUI.SetActive(true);

    }
}
