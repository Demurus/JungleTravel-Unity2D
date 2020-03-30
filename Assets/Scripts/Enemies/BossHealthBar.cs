using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour
{
    [SerializeField] private Boss _boss;
    private Image _healthbar;
    private float _bossHealth;

    public static float FillAmountHealth;
    
    private void Awake()
    {
        _healthbar = GetComponent<Image>();                             
    }
    void Start()

    {
        FillAmountHealth = _bossHealth;
    }

    public void Refresh(float lives)
    {
       _bossHealth = lives;
       _healthbar.fillAmount = _bossHealth / FillAmountHealth; 
    }
}
