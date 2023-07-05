using UnityEngine;
using UnityEngine.Events;

namespace  RPG.LevelStats
{
    public class XPTracker : MonoBehaviour
    {
        [SerializeField] private BaseXPTranslation xpTranslationType;
        [SerializeField] UnityEvent<int, int> OnLevelChanged = new UnityEvent<int, int>();
    
        private BaseXPTranslation _xpTranslation;


        private void Awake()
        {
            _xpTranslation = ScriptableObject.Instantiate(xpTranslationType);
        }

        // Update is called once per frame
        public void AddXP(int amount)
        {
            int previousLevel = _xpTranslation.CurrentLevel;
            if (_xpTranslation.AddXP(amount))
            {
                OnLevelChanged.Invoke(previousLevel,_xpTranslation.CurrentLevel);
            }

        }

        public void SetLevel(int level)
        {
            _xpTranslation.SetLevel(level);
        }

        public int GetLevel()
        {
            return _xpTranslation.CurrentLevel;
        }
        public int GetCurrentXP()
        {
            return _xpTranslation.CurrentXP;
        }
        public int GetXPRequiredForNextLevel()
        {
            return _xpTranslation.XPRequiredForNextLevel;
        }

    }
}

    