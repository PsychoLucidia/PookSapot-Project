using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitbox : MonoBehaviour
{
    public SpiderStat spiderStat;
    public bool isEnemy;

    [Header("Data")]
    [SerializeField] GameObject _spiderObj;
    [SerializeField] SpiderStat _spiderStat;
    [SerializeField] EnemyHurtbox _enemyHurtbox;
    [SerializeField] PlayerHurtbox _playerHurtbox;

    /// <summary>
    /// Called when the script instance is being loaded.
    /// This function initializes the spiderStat variable based on the isEnemy flag.
    /// If isEnemy is false, it retrieves the SpiderStat component from the "Player" game object.
    /// If isEnemy is true, it retrieves the SpiderStat component from the "Enemy" game object.
    /// It then sets the game object to be inactive.
    /// </summary>
    void Awake()
    {
        if (!isEnemy) { spiderStat = GameObject.Find("Player(Clone)").GetComponent<SpiderStat>(); }
        else { spiderStat = GameObject.Find("Enemy(Clone)").GetComponent<SpiderStat>(); }

        this.gameObject.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Called when the trigger collider is touched by another collider.
    /// 
    /// This function handles the collision with "EnemyHurtbox" or "PlayerHurtbox" game objects.
    /// It retrieves the SpiderStat component from the parent game object and applies damage.
    /// It also retrieves the EnemyHurtbox or PlayerHurtbox component and calls the LightIntensityTween function.
    /// 
    /// Parameters:
    /// other (Collider): The collider that touched the trigger collider.
    /// </summary>
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("EnemyHurtbox"))
        {
            _spiderObj = other.transform.parent.gameObject;

            _spiderStat = _spiderObj.GetComponent<SpiderStat>();
            _enemyHurtbox = _spiderObj.transform.Find("Hurtbox").GetComponent<EnemyHurtbox>();

            if (_spiderStat != null) { _spiderStat.TakeDamage(spiderStat.spiderDamage); }
            else { Debug.LogError("Spider Stat not found on enemy"); }

            if (_enemyHurtbox != null) { _enemyHurtbox.LightIntensityTween(45f); }
        }

        if (other.gameObject.CompareTag("PlayerHurtbox"))
        {
            _spiderObj = other.transform.parent.gameObject;

            _spiderStat = _spiderObj.GetComponent<SpiderStat>();
            _playerHurtbox = _spiderObj.transform.Find("Hurtbox").GetComponent<PlayerHurtbox>();

            if (_spiderStat != null) { _spiderStat.TakeDamage(spiderStat.spiderDamage); }
            else { Debug.LogError("Spider Stat not found on player"); }

            if (_playerHurtbox != null) { _playerHurtbox.LightIntensityTween(45f); }
        }
    }
}
