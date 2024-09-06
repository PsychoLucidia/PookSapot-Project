using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneManager : MonoBehaviour
{
    public static CutsceneManager instance;

    public CutscenePathPoints[] definedPathPoints;
    public Transform lookAt;
    public Transform positionAt;

    public GameObject virtualCam;

    void Awake()
    {
        lookAt = GameObject.Find("LookAt").transform;
        positionAt = GameObject.Find("PositionAt").transform;

        Transform cameraRoot = GameObject.Find("Main Camera").transform;
        virtualCam = cameraRoot.transform.Find("VCAM2").gameObject;
        
    }

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        GameManager.instance.gameState = GameState.Cutscene;
        StartCutscene();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartCutscene()
    {
        Fader.instance.gameObject.SetActive(true);
        Fader.instance.faderCG.alpha = 1;
        Fader.instance.FadeEnable(0, 1f, false, 0);

        positionAt.position = new Vector3(0, 54.8f, 82.5f);

        LeanTween.moveSplineLocal(positionAt.gameObject, definedPathPoints[0].pathPoints, 5f).setEaseInOutCubic().setOnComplete(() => 
        {
            virtualCam.SetActive(false);
            Invoke("TriggerBattleStart", 1f);
        });
    }

    void TriggerBattleStart()
    {
        UiManager.instance.ActivateObject(1);
    }

}


[System.Serializable]
public class CutscenePathPoints
{
    public Vector3[] pathPoints;
}
