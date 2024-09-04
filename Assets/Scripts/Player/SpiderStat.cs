using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpiderStat : MonoBehaviour
{
    [Header("Stats")]   // The set stats for the spiders
    public int health;
    public int maxHealth = 100;
    public float currentStamina;
    public float stamina = 60f;
    public int spiderDamage;
    public int spiderDefense;

    [Header("Bools")]
    public bool isEnemy;
    public bool isInvulnerable = false;

    [Header("Settings")]
    public float staminaCooldown = 1f;
    public float curStaminaCooldown = 0f;
    public float regenRate = 8f;
    public float staminaConsume = 15f;

    public Image spiderHealth;

    [Header("Components (Private)")]
    [SerializeField] BattleUI battleUI;

    void Awake()
    {
        if (!isEnemy)
        {
            Transform canvasRoot = GameObject.Find("CanvasStatic").transform;
            Transform battleUIRoot = canvasRoot.transform.Find("BattleUI");
            spiderHealth = battleUIRoot.transform.Find("SpiderHealth").GetComponent<Image>();

            battleUI = battleUIRoot.GetComponent<BattleUI>();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        currentStamina = stamina;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isEnemy) { HealthStatus(); }

        StatClamp();

        StaminaRegen();
        
    }

    public void Attack()    // is called once the player or AI attacks
    {
        currentStamina -= staminaConsume;   // reduces the stamina when the player or AI attacks
        curStaminaCooldown = staminaCooldown;   // sets the timer of stamina cooldown to 1 second before it starts regenrating
    }

    public void TakeDamage(int damage)
    {
        if (!isInvulnerable)    // determines if the player or AI is not in an invulnerable state
        {
            health -= damage - (spiderDefense / 2); // the amount of health to be reduced when attacked
            isInvulnerable = true;  // makes the player or AI invulnerable from attacks
            Invoke("ResetInvulnerability", 0.5f);   // the player or AI will be invulnerable for 0.5 seconds

            if (!isEnemy)
            {
                battleUI.ShakeHealth(); // the UI of the spider's health does a shake animation
            }
        }
    }

    void StatClamp()
    {
        currentStamina = Mathf.Clamp(currentStamina, 0f, stamina);
        health = Mathf.Clamp(health, 0, maxHealth);
    }

    void StaminaRegen() // regenerates the stamina bar after some time if the player or AI stopped attacking
    {
        if (curStaminaCooldown > 0)
        {
            curStaminaCooldown -= Time.deltaTime;   // reduces the cooldown per second
        }

        if (currentStamina < stamina && curStaminaCooldown <= 0)
        {
            currentStamina += regenRate * Time.deltaTime;   // regenerates stamina over time
        }

    }   

    void HealthStatus()
    {
        if (health >= (maxHealth * 0.60))   // color of health if it is below 100 but above 60 percent
        {
            spiderHealth.color = Color.green;
        }
        else if (health >= (maxHealth * 0.20))  // color of health if it is below 60 but above 20 percent
        {
            spiderHealth.color = Color.yellow;
        }
        else
        {
            spiderHealth.color = Color.red; // color of health if it is below 20 percent
        }
    }

    void ResetInvulnerability()
    {
        isInvulnerable = false;
    }
}
