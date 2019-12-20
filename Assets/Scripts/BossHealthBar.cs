using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour
{
    
    Image healthbar;
    float BossHealth;
    public static float fillAmountHealth;
    private Boss boss;
    private void Awake()
    {
            boss = FindObjectOfType<Boss>();
            BossHealth = boss.Lives;
    }
    void Start()

    {
         healthbar = GetComponent<Image>();
        fillAmountHealth = BossHealth;
    }

    public void Refresh()
    {
            BossHealth = boss.Lives;
            healthbar.fillAmount = BossHealth / fillAmountHealth; 
    }
}
