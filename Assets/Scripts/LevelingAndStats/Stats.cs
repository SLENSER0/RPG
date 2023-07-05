using System;
using RPG.Core;
using RPG.Combat;
using UnityEngine;

namespace RPG.LevelStats
{
    public class Stats : MonoBehaviour
    {
        [SerializeField] private int xpReward;
        
        [Header("Health & Mana")] 
        [SerializeField] private float currentHealthPoints = 100f;

        [SerializeField] private float maxHealth = 100f;
        [SerializeField] private float currentManaPoints = 0;
        [SerializeField] private float maxMana;
        
        [Header("Stats")] 
        [SerializeField] private int strength = 0;
        [SerializeField] private int stamina = 0;
        [SerializeField] private int intelligence = 0;

        [Header("Skills")]
        [SerializeField] private int statPoints = 0;

        [SerializeField] private int statsPerLevel = 5;

        [Header("Conversions")] 
        [SerializeField] private int staminaToHealthConversion = 10;
        [SerializeField] private int strengthToDamageConversion = 2;
        [SerializeField] private int intelligenceToManaConversion = 10;


        private float _baseHealth = 100;
        private float _baseMana = 100;
        private float _baseDamage = 5;
        
        private bool _isDead = false;

        private GameObject _player;

        private void Start()
        {
            _player = GameObject.FindGameObjectWithTag("Player");
        }

        private void Update()
        {
            if (!gameObject.CompareTag("Player")) return;
            maxHealth = _baseHealth + stamina * staminaToHealthConversion;
            maxMana = _baseMana + intelligence * intelligenceToManaConversion;
            _player.GetComponent<Fighter>().SetWeaponDamage((int)_baseDamage+strength*strengthToDamageConversion);
        }

        public bool IsDead()
        {
            return _isDead;
        }

        public void TakeDamage(float damage)
        {
            currentHealthPoints = Mathf.Max(currentHealthPoints - damage, 0);
            if (currentHealthPoints == 0) Die();
        }

        public void ReduceMana(float mana)
        {
            currentManaPoints = Mathf.Max(currentManaPoints - mana, 0);
        }

        public float GetHealth()
        {
            return currentHealthPoints;
        }

        public float GetMaxHealth()
        {
            return maxHealth;
        }

        public float GetMana()
        {
            return currentManaPoints;
        }
        
        public float GetMaxMana()
        {
            return maxMana;
        }

        private void Die()
        {
            if (_isDead) return;
            _isDead = true;
            _player.GetComponent<XPTracker>().AddXP(xpReward);
            GetComponent<Animator>().SetTrigger("die");
            GetComponent<ActionScheduler>().CancelCurrentAction();
            
            
        }

        public void OnUpdateLevel(int previousLevel, int currentLevel)
        {
            statPoints += statsPerLevel;
            print(previousLevel);
            print(currentLevel);
        }

        public void IncreaseStat(ref int stat, int amount = 1)
        {
            stat += Math.Min(amount, statPoints);
            statPoints -= Math.Min(amount, statPoints);
            
        }

        public void IncreaseStamina(int amount = 1)
        {
            IncreaseStat(ref stamina, amount);
            
        }

        public void IncreaseStrength(int amount = 1)
        {
            IncreaseStat(ref strength, amount);
        } 

        public void IncreaseIntelligence(int amount = 1)
        {
            IncreaseStat(ref intelligence, amount);
        }
    }
}