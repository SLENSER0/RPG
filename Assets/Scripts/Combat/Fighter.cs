using RPG.Core;
using RPG.LevelStats;
using RPG.Movement;
using UnityEngine;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] private float weaponRange = 2f;
        [SerializeField] float timeBetweenAttacks = 1f;
        [SerializeField] private float weaponDamage = 5f;
        [SerializeField] private Projectile projectile;
        [SerializeField] private Transform pointForSpawnProjectile;
        
        private Stats _target;
        private float _timeSinceLastAttack = Mathf.Infinity;
        private void Update()
        {
            _timeSinceLastAttack += Time.deltaTime;  
            
            if (_target == null ) return;
            if (_target.IsDead()) return;
            if (!GetIsInRange())
            {
                GetComponent<Mover>().MoveTo(_target.transform.position);
            }
            else
            {
                
                GetComponent<Mover>().Cancel();
                AttackBehaviour();
            }
        }

        private void AttackBehaviour()
        {
            transform.LookAt(_target.transform);
            if (_timeSinceLastAttack > timeBetweenAttacks)
            {
                TriggerAttack();
                _timeSinceLastAttack = 0;


            }
            
        }

        private void TriggerAttack()
        {
            GetComponent<Animator>().ResetTrigger("stopAttack");
            GetComponent<Animator>().SetTrigger("attack");
        }

        
        // Animation event
        private void Hit()
        {
            if (_target == null) return;
            if (projectile == null)
            {
                _target.TakeDamage(weaponDamage);
            }
            else
            {
                LaunchProjectile();
            }
        }

        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, _target.transform.position) < weaponRange;
        }

        private void LaunchProjectile()
        {
            Projectile projectileInstance = Instantiate(projectile, pointForSpawnProjectile.position, pointForSpawnProjectile.rotation);
            projectileInstance.SetTarget(_target, weaponDamage);
        }

        public bool CanAttack(GameObject combatTarget)
        {
            if (combatTarget == null) return false;
            if (combatTarget == gameObject) return false;
            Stats targetToTest = combatTarget.GetComponent<Stats>();
            return targetToTest != null && !targetToTest.IsDead();
        }


        public void Attack(GameObject combatTarget)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            _target = combatTarget.GetComponent<Stats>();
        }

        public void Cancel()
        {
            StopAttack();
            _target = null;
        }

        private void StopAttack()
        {
            GetComponent<Animator>().ResetTrigger("attack");
            GetComponent<Animator>().SetTrigger("stopAttack");
        }

        public void SetWeaponDamage(int newDamage)
        {
            weaponDamage = newDamage;
        }


    }
}

