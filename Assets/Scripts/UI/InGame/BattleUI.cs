using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleUI : MonoBehaviour
{
    public Transform spiderHealth;
    public SpiderStat spiderStat;
    public CanvasGroup canvasGroup;

    public Vector3 healthPosition;
    public float shakeMagnitude;
    public float shakeDuration;

    void Awake()
    {
        canvasGroup = this.gameObject.GetComponent<CanvasGroup>();
        spiderStat = GameObject.Find("Player(Clone)").GetComponent<SpiderStat>();

        healthPosition = spiderHealth.transform.localPosition;
        canvasGroup.alpha = 0;

    }

    // Start is called before the first frame update
    void Start()
    {

    }

    void OnEnable() // enables the battle UI fade in animation
    {
        canvasGroup.alpha = 0;
        FadeIn(1, 0.2f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShakeHealth()   // creates a shake animation to the health whenever the player takes damage
    {
        LeanTween.moveLocal(spiderHealth.gameObject, healthPosition + 
            new Vector3(Random.Range(-shakeMagnitude, shakeMagnitude), Random.Range(-shakeMagnitude, shakeMagnitude), 0), shakeDuration / 10f)
                .setLoopPingPong(10).setOnComplete(() => { spiderHealth.transform.localPosition = healthPosition; });
    }

    public void FadeIn(float alpha, float time) 
    {
        LeanTween.alphaCanvas(canvasGroup, alpha, time);
    }

    void OnDisable()    // stops the animation
    {
        LeanTween.cancel(this.gameObject);
        LeanTween.cancel(spiderHealth.gameObject);
    }
}
