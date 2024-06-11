using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;
using Slider = UnityEngine.UI.Slider;

public class HealthBar : MonoBehaviour
{      
    Damagable playerDamagable;
    public TMP_Text healthText;
    public Slider healthSlider;

    // Start is called before the first frame update
    private void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player==null)
        {
         Debug.Log("No player found");   
        }
        playerDamagable = player.GetComponent<Damagable>();
        
    }

    void Start()
    {
        healthSlider.value = CalculateSliderPersantage(playerDamagable.Health,playerDamagable.MaxHealth);
        healthText.text =  playerDamagable.Health + "/" + playerDamagable.MaxHealth;
    }

    private void OnEnable()
    {
        playerDamagable.healthChanged.AddListener(OnPlayerHealthChanged);
    }

    private void OnDisable()
    {
        playerDamagable.healthChanged.RemoveListener(OnPlayerHealthChanged);
    }

    private float CalculateSliderPersantage(float currentHealth,float maxHealth)
    {
        return  currentHealth / maxHealth;
    } 
    

    private void OnPlayerHealthChanged(int newHealth,int maxHealth)
    {
        healthSlider.value = CalculateSliderPersantage(newHealth,maxHealth);
        healthText.text =  newHealth + "/" + maxHealth;
    }
}
