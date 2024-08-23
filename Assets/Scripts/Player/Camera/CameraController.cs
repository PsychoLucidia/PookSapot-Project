using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public CinemachineVirtualCamera vcam;
    public Transform player;
    public Transform lookAtPos;
    CinemachineTransposer transposer;

    public float camDistance = 10f;

    // Start is called before the first frame update
    void Start()
    {
        Transform rootObject = this.gameObject.transform;

        player = GameObject.Find("Player").transform;
        lookAtPos = GameObject.Find("LookAt").transform;
        vcam = rootObject.transform.Find("Virtual Camera").GetComponent<CinemachineVirtualCamera>();

        transposer = vcam.GetCinemachineComponent<CinemachineTransposer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.z > 0)
        {
            SetCameraAngle(new Vector3(transposer.m_FollowOffset.x, transposer.m_FollowOffset.y, 3.82f));
        }
        else
        {
            SetCameraAngle(new Vector3(transposer.m_FollowOffset.x, transposer.m_FollowOffset.y, -3.82f));
        }

        if (player.transform.position.x > lookAtPos.position.x)
        {
            SetCameraPosition(new Vector3(camDistance, transposer.m_FollowOffset.y, transposer.m_FollowOffset.z));
        }
        else
        {
            SetCameraPosition(new Vector3(-camDistance, transposer.m_FollowOffset.y, transposer.m_FollowOffset.z));
        }
    }

    public void SetCameraAngle(Vector3 pos)
    {
        transposer.m_FollowOffset = new Vector3(transposer.m_FollowOffset.x, transposer.m_FollowOffset.y, Mathf.Lerp(transposer.m_FollowOffset.z, pos.z, 4f * Time.deltaTime));
    }

    public void SetCameraPosition(Vector3 pos)
    {
        transposer.m_FollowOffset = new Vector3(Mathf.Lerp(transposer.m_FollowOffset.x, pos.x, 10f * Time.deltaTime), transposer.m_FollowOffset.y, transposer.m_FollowOffset.z);
    }
}
