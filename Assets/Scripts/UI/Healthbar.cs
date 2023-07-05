using RPG.LevelStats;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.UI
{
    public class Healthbar : MonoBehaviour
    {
        private Camera _mainCamera;
        private Stats _stats;
        private Slider _slider;
        private TextMeshProUGUI _textMesh;

        void Start()
        {
            _mainCamera = Camera.main;
            _stats = GetComponentInParent<Stats>();
            _slider = GetComponentInChildren<Slider>();
            _textMesh = GetComponentInChildren<TextMeshProUGUI>();

            _slider.maxValue = _stats.GetHealth();
        }
        void LateUpdate()
        {
            if (_stats.IsDead())
            {
                Destroy(gameObject);
                return;
            }
        
            _slider.value = _stats.GetHealth();
            _textMesh.text = _stats.GetHealth().ToString();
            transform.LookAt(transform.position + _mainCamera.transform.rotation * Vector3.forward, 
                _mainCamera.transform.rotation * Vector3.up);

        }
    }
}

