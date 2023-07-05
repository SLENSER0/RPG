using RPG.LevelStats;
using TMPro;
using UnityEngine;

namespace RPG.UI
{
    public class PlayerUILevel : MonoBehaviour
    {
        private GameObject _player;
        private XPTracker _XPTracker;
        private TextMeshProUGUI _textMesh;
        void Start()
        {
            _player = GameObject.FindGameObjectWithTag("Player");
            _XPTracker = _player.GetComponent<XPTracker>();
            _textMesh = GetComponentInChildren<TextMeshProUGUI>();

        }

        void Update()
        {
            _textMesh.text = _XPTracker.GetLevel().ToString();

        }
    }
}
