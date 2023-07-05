using RPG.LevelStats;
using UnityEngine;

namespace RPG.Combat
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private Stats target = null;
        [SerializeField] private bool isHoming = true;
        [SerializeField] private float speed = 1;

        private float _damage=10f;

        private void Start()
        {
            transform.LookAt(GetAimLocation());
        }

        private void Update()
        {
            if (target == null) return;
            if (isHoming && !target.IsDead())
            {
                transform.LookAt(GetAimLocation());
            }
        
            transform.Translate(Vector3.forward*speed*Time.deltaTime);
        }

        private Vector3 GetAimLocation()
        {
            CapsuleCollider targetCapsule = target.GetComponent<CapsuleCollider>();
            if (targetCapsule == null)
            {
                return target.transform.position;
            }

            return target.transform.position + Vector3.up * targetCapsule.height / 2;
        }

        public void SetTarget(Stats target, float damage)
        {
            this.target = target;
            _damage = damage;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Stats>() != target) return;
            if (target.IsDead()) return;
            target.TakeDamage(_damage);
            Destroy(gameObject);
        }
    }
}
