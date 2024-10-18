using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotlightLookAt : MonoBehaviour
{
    public Transform player;
    public Transform enemy;

    public bool isEnemy = false;
    // Start is called before the first frame update
    void Start()
    {
        /*
        player = GameObject.Find("Player(Clone)").transform;
        if (player == null)
        {
            Debug.LogWarning("Player not Found");
        }
        enemy = GameObject.Find("Enemy(Clone)").transform;
        if (enemy == null)
        {
            Debug.LogWarning("Enemy not Found");
        }
        */

        StartCoroutine(AttachReference());
    }

    // Update is called once per frame
    void Update()
    {
        if (isEnemy)
        {
            if (enemy != null)
            {
                transform.LookAt(enemy.transform);
            }
        }
        else
        {
            if (player != null)
            {
                transform.LookAt(player.transform);
            }
        }
    }

    IEnumerator AttachReference()
    {
        yield return new WaitForSeconds(0.1f);

        player = GameObject.Find("Player(Clone)").transform;
        enemy = GameObject.Find("Enemy(Clone)").transform;
    }
}
