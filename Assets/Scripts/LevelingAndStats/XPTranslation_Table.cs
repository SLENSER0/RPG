using System.Collections.Generic;
using UnityEngine;

namespace RPG.LevelStats
{
    [System.Serializable]
    public class XPTranslationTableEntry
    {
        public int Level;
        public int XPRequired;
    }
    [CreateAssetMenu(menuName = "Stats/XP Table", fileName = "XPTranslation_Table")]
    public class XPTranslation_Table : BaseXPTranslation
    {
        [SerializeField] List<XPTranslationTableEntry> table;
        
        public override bool AddXP(int amout)
        {
            if (AtLevelCap) return false;
    
            CurrentXP += amout;
            
            for (int index = table.Count - 1; index >= 0; index--)
            {
                var entry = table[index];
    
                // found a matching entry
                if (CurrentXP >= entry.XPRequired)
                {
                    // level changed?
                    if (entry.Level != CurrentLevel)
                    {
                        CurrentLevel = entry.Level;
    
                        AtLevelCap = table[^1].Level == CurrentLevel;
    
                        return true;
                    }
                    break;
                }
            }
            return false;
        }
    
        public override void SetLevel(int level)
        {
            CurrentXP = 0;
            CurrentLevel = 1;
            AtLevelCap = false;
    
            foreach (var entry in table)
            {
                if (entry.Level == level)
                {
                    AddXP(entry.XPRequired);
                    return;
                }
            }
    
            throw new System.ArgumentOutOfRangeException($"Could not find any entry for level {level}");
        }
    
        protected override int GetXPRequiredForNextLevel()
        {
            if (AtLevelCap)
                return int.MaxValue;
    
            for(int index = 0; index < table.Count; ++index)
            {
                var entry = table[index];
    
                if (entry.Level == CurrentLevel)
                    return table[index + 1].XPRequired - CurrentXP;
            }
    
            throw new System.ArgumentOutOfRangeException($"Could not find any entry for level {CurrentLevel}");
        }
    }
}

