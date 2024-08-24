using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHurtbox : MonoBehaviour
{
    [SerializeField] Light orangeLight;
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
        orangeLight.intensity = intensity;
        LeanTween.value(orangeLight.intensity, 0, 0.3f).setOnUpdate((float val) => { orangeLight.intensity = val;}).setEaseOutCubic();
    }
}
