using RPG.LevelStats;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.UI
{
    public class PlayerUIXPBar : MonoBehaviour
    {
        private GameObject _player;
        private XPTracker _XPTracker;
        private Slider _slider;
        void Start()
        {
            _player = GameObject.FindGameObjectWithTag("Player");
            _XPTracker = _player.GetComponent<XPTracker>();
            _slider = GetComponent<Slider>();

        }

        void Update()
        {
            _slider.maxValue = _XPTracker.GetXPRequiredForNextLevel() + _XPTracker.GetCurrentXP();
            _slider.value = _XPTracker.GetCurrentXP();
        }
    }
}
