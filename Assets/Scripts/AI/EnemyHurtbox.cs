using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHurtbox : MonoBehaviour
{
    [SerializeField] Light orangeLight;
    [SerializeField] SpiderStat spiderStat;
    [SerializeField] float colorFloat;
    
    // Start is called before the first frame update
    void Start()
    {
        orangeLight = this.gameObject.GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LightIntensityTween(float intensity)
    {
        if (spiderStat.health >= (spiderStat.maxHealth * 0.60))   // color of health if it is below 100 but above 60 percent
        {
            orangeLight.color = Color.green;
        }
        else if (spiderStat.health >= (spiderStat.maxHealth * 0.25))  // color of health if it is below 60 but above 20 percent
        {
            orangeLight.color = Color.yellow;
        }
        else
        {
            orangeLight.color = Color.red; // color of health if it is below 20 percent
        }

        orangeLight.intensity = intensity;
        LeanTween.value(orangeLight.intensity, 0, 0.3f).setOnUpdate((float val) => { orangeLight.intensity = val;}).setEaseOutCubic();
    }
}
