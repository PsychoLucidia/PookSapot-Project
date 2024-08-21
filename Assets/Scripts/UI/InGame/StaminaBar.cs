using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaminaBar : MonoBehaviour
{
    [Header("Debug")]
    [SerializeField] Vector3 _screenPosition;
    [SerializeField] Vector3 _runtimeUIOffset;

    [Header("Components (Public)")]
    public Transform objectPos;
    public Vector3 uiOffset;

    [Header("Components (Private)")]
    [SerializeField] RectTransform _uiElement;
    [SerializeField] Camera _cam;
    [SerializeField] Transform _playerTransform;

    void Awake()
    {
        ComponentInit();
    }

    // Start is called before the first frame update
    void Start()
    {
        _runtimeUIOffset = uiOffset;
    }

    // Update is called once per frame
    void Update()
    {
        _screenPosition = _cam.WorldToScreenPoint(objectPos.position + _runtimeUIOffset);

        _uiElement.position = new Vector2(_screenPosition.x, _screenPosition.y);

        SetStaminaPosition(uiOffset.z);
    }

    void ComponentInit()
    {
        Transform transRoot = GameObject.Find("Player").transform;
        objectPos = transRoot.transform.Find("StaminaPos");

        Transform barRoot = this.gameObject.transform;
        _uiElement = barRoot.transform.Find("Stamina").GetComponent<RectTransform>();
        
        _cam = Camera.main;

        _playerTransform = GameObject.Find("Player").transform;
    }

    void SetStaminaPosition(float value)
    {
        if (_playerTransform.position.x > 0)
        {
            _runtimeUIOffset.z = Mathf.Lerp(_runtimeUIOffset.z, value, 10f * Time.deltaTime);
        }
        else
        {
            _runtimeUIOffset.z = Mathf.Lerp(_runtimeUIOffset.z, -value, 10f * Time.deltaTime);
        }


    }
}
