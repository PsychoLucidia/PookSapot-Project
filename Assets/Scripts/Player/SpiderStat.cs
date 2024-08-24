using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpiderStat : MonoBehaviour
{
    [Header("Stats")]
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

    public void Attack()
    {
        currentStamina -= staminaConsume;
        curStaminaCooldown = staminaCooldown;
    }

    public void TakeDamage(int damage)
    {
        if (!isInvulnerable)
        {
            health -= damage - (spiderDefense / 2);
            isInvulnerable = true;
            Invoke("ResetInvulnerability", 0.5f);

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

    void StaminaRegen()
    {
        if (curStaminaCooldown > 0)
        {
            curStaminaCooldown -= Time.deltaTime;
        }

        if (currentStamina < stamina && curStaminaCooldown <= 0)
        {
            currentStamina += regenRate * Time.deltaTime;
        }

    }   

    void HealthStatus()
    {
        if (health >= (maxHealth * 0.60))
        {
            spiderHealth.color = Color.green;
        }
        else if (health >= (maxHealth * 0.20))
        {
            spiderHealth.color = Color.yellow;
        }
        else
        {
            spiderHealth.color = Color.red;
        }
    }

    void ResetInvulnerability()
    {
        isInvulnerable = false;
    }
}
