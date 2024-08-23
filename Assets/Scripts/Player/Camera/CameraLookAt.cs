using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLookAt : MonoBehaviour
{
    public Transform playerObj;
    public Transform enemyObj;

    Vector3 centerPos;

    // Start is called before the first frame update
    void Start()
    {
        if (playerObj == null)
        {
            playerObj = GameObject.Find("Player").transform;
        }

        if (enemyObj == null)
        {
            enemyObj = GameObject.Find("Enemy").transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        GameObjectCenter();
        this.transform.position = new Vector3(centerPos.x, this.transform.position.y, centerPos.z);
    }

    void GameObjectCenter()
    {
        centerPos = (playerObj.position + enemyObj.position) / 2;
    }
}
