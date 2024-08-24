using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    [Header("Debug")]
    [SerializeField] Vector3 _screenPosition;
    [SerializeField] Vector3 _runtimeUIOffset;
    [SerializeField] float _fillRatio;

    [Header("Components (Public)")]
    public Transform objectPos;
    public Vector3 uiOffset;
    public Image staminaBar;
    public SpiderStat spiderStat;

    [Header("Components (Private)")]
    [SerializeField] RectTransform _uiElement;
    [SerializeField] Camera _cam;
    [SerializeField] Transform _playerTransform;
    [SerializeField] Transform _lookAtPos;
    [SerializeField] CanvasGroup _canvasGroup;
    [SerializeField] bool _isVisible = false;

    void Awake()
    {
        ComponentInit();
    }

    // Start is called before the first frame update
    void Start()
    {
        _runtimeUIOffset = uiOffset;
        _canvasGroup.alpha = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        StaminaBarFill();
        FadeUI();

        _screenPosition = _cam.WorldToScreenPoint(objectPos.position + _runtimeUIOffset);

        _uiElement.position = new Vector2(_screenPosition.x, _screenPosition.y);

        SetStaminaPosition(uiOffset.z);
    }

    void ComponentInit()
    {
        // Get Components (objectPos)
        Transform transRoot = GameObject.Find("Player").transform;
        Transform modelTransform = transRoot.transform.Find("Model");
        objectPos = modelTransform.transform.Find("StaminaPos");

        // Get Components (UI)
        Transform barRoot = this.gameObject.transform;
        _uiElement = barRoot.transform.Find("Stamina").GetComponent<RectTransform>();

        Transform staminaTransform = barRoot.transform.Find("Stamina");
        staminaBar = staminaTransform.transform.Find("Front").GetComponent<Image>();

        _canvasGroup = this.gameObject.GetComponent<CanvasGroup>();

        _lookAtPos = GameObject.Find("LookAt").transform;
        
        _cam = Camera.main;

        _playerTransform = GameObject.Find("Player").transform;
        
        spiderStat = GameObject.Find("Player").GetComponent<SpiderStat>();
    }

    void SetStaminaPosition(float value)
    {
        if (_playerTransform.position.x > _lookAtPos.position.x)
        {
            _runtimeUIOffset.z = Mathf.Lerp(_runtimeUIOffset.z, value, 10f * Time.deltaTime);
        }
        else
        {
            _runtimeUIOffset.z = Mathf.Lerp(_runtimeUIOffset.z, -value, 10f * Time.deltaTime);
        }
    }

    void StaminaBarFill()
    {
        _fillRatio = spiderStat.currentStamina / spiderStat.stamina;

        staminaBar.fillAmount = _fillRatio;
    }

    void FadeUI()
    {
        if (_fillRatio < 1f && !_isVisible)
        {
            LeanTween.alphaCanvas(_canvasGroup, 1f, 0.05f);
            _isVisible = true;
        }
        else if (_fillRatio >= 1f && _isVisible)
        {
            LeanTween.alphaCanvas(_canvasGroup, 0f, 0.1f);
            _isVisible = false;
        }
    }
}
