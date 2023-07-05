using RPG.LevelStats;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.UI
{
    public class PlayerUIHealthBar : MonoBehaviour
    {
        private GameObject _player;
        private Slider _slider;

        void Start()
        {
            _player = GameObject.FindGameObjectWithTag("Player");
            _slider = GetComponent<Slider>();
        
        }


        void Update()
        {
            if (_player.GetComponent<Stats>().IsDead()) return;
            _slider.maxValue = _player.GetComponent<Stats>().GetMaxHealth();
            _slider.value = _player.GetComponent<Stats>().GetHealth();

        }
    }
}
