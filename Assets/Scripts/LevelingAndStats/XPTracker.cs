using System;
using UnityEngine;

public class XPTracker : MonoBehaviour
{
    [SerializeField] private BaseXPTranslation xpTranslationType;
    
    private BaseXPTranslation _xpTranslation;


    private void Awake()
    {
        _xpTranslation = ScriptableObject.Instantiate(xpTranslationType);
    }

    // Update is called once per frame
    public void AddXP(int amount)
    {
        _xpTranslation.AddXP(amount);
        print(_xpTranslation.XPRequiredForNextLevel);
        print(_xpTranslation.CurrentXP);
        print(_xpTranslation.CurrentLevel);

    }

    public void SetLevel(int level)
    {
        _xpTranslation.SetLevel(level);
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        
    }
}
    