using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpiderStat : MonoBehaviour
{
    [Header("Stats")]
    public int maxHealth = 100;
    public int health;
    public float stamina;
    public float currentStamina;
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

    [Header("Components (Private)")]
    [SerializeField] BattleUI battleUI;

    [Header("Components (Public)")]
    public Image spiderHealth;
    void Awake()
    {
        if (!isEnemy)
        {
            Transform canvasRoot = GameObject.Find("CanvasStatic").transform;
            Transform battleUIRoot = canvasRoot.transform.Find("BattleUI");
            spiderHealth = battleUIRoot.transform.Find("SpiderHealth").GetComponent<Image>();

            battleUI = battleUIRoot.GetComponent<BattleUI>();
        }

        health = maxHealth;
        currentStamina = stamina;
    }

    // Start is called before the first frame update
    void Start()
    {
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

    /// <summary>
    /// Applies damage to the player or AI.
    /// <para>
    /// If the player or AI is not in an invulnerable state, the health is reduced by the given amount minus half of the spider's defense.
    /// The player or AI is then made invulnerable for 0.5 seconds and the health UI is shaken.
    /// </para>
    /// </summary>
    /// <param name="damage">The amount of damage to apply.</param>
    public void TakeDamage(int damage)
    {
        // Check if the player or AI is not in an invulnerable state
        if (!isInvulnerable)
        {
            // Reduce the health of the player or AI by the given amount minus half of the spider's defense
            health -= damage - (spiderDefense / 2);

            // Make the player or AI invulnerable from attacks
            isInvulnerable = true;

            // Set a timer to make the player or AI invulnerable for 0.5 seconds
            Invoke("ResetInvulnerability", 0.5f);

            // If the player or AI is not an enemy, shake the health UI
            if (!isEnemy)
            {
                battleUI.ShakeHealth();
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
        else if (health >= (maxHealth * 0.25))  // color of health if it is below 60 but above 20 percent
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
