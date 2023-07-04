using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "Stats/XP Linear", fileName = "XPTranslation_Linear")]
public class XPTranslation_Linear : BaseXPTranslation
{
    [SerializeField] int offset = 100;
    [SerializeField] float slope = 50;
    [SerializeField] int levelCap = 10;
    
    protected int XPForLevel(int level)
    {
        return Mathf.FloorToInt((Mathf.Min(levelCap, level) - 1) * slope + offset);
    }
    public override bool AddXP(int amount)
    {
        
        if (AtLevelCap)
            return false;

        CurrentXP += amount;

        int newLevel = Mathf.Min(Mathf.FloorToInt((CurrentXP - offset) / slope) + 1, levelCap);
        bool levelledUp = newLevel != CurrentLevel;

        CurrentLevel = newLevel;
        AtLevelCap = CurrentLevel == levelCap;

        return levelledUp;
    }

    public override void SetLevel(int level)
    {
        CurrentXP = 0;
        CurrentLevel = 1;
        AtLevelCap = false;

        AddXP(XPForLevel(level));
    }

    protected override int GetXPRequiredForNextLevel()
    {
        if (AtLevelCap)
            return int.MaxValue;

        return XPForLevel(CurrentLevel + 1) - CurrentXP;
    }
}
