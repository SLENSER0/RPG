using UnityEngine;

namespace RPG.LevelStats
{
    public abstract class BaseXPTranslation : ScriptableObject
    {
        public int CurrentXP { get; protected set; } = 0;
        public int CurrentLevel { get; protected set; } = 1;
        public int XPRequiredForNextLevel => GetXPRequiredForNextLevel();
        public bool AtLevelCap { get; protected set; } = false;

        public abstract bool AddXP(int amout);
        public abstract void SetLevel(int level);
        protected abstract int GetXPRequiredForNextLevel();

    }
}

