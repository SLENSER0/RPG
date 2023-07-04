using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Core;
using RPG.LevelStats;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    private Camera _mainCamera;
    private Stats _health;
    private Slider _slider;
    private TextMeshProUGUI _textMesh;

    void Start()
    {
        _mainCamera = Camera.main;
        _health = GetComponentInParent<Stats>();
        _slider = GetComponentInChildren<Slider>();
        _textMesh = GetComponentInChildren<TextMeshProUGUI>();

        _slider.maxValue = _health.GetHealth();
    }
    // Update is called once per frame
    void LateUpdate()
    {
        _slider.value = _health.GetHealth();
        _textMesh.text = _health.GetHealth().ToString();
        transform.LookAt(transform.position + _mainCamera.transform.rotation * Vector3.forward, 
            _mainCamera.transform.rotation * Vector3.up);
        
        

    }
}
