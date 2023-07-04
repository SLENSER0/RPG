using RPG.Combat;
using RPG.LevelStats;
using RPG.Movement;
using UnityEngine;

namespace RPG.Control
{ 
    public class AIController : MonoBehaviour
    {
        [SerializeField] private float chaseDistance = 5f;

        private Fighter _fighter;
        private Stats _stats;
        private Mover _mover;
        private GameObject _player;

        private Vector3 _guardPosition;

        private void Start()
        {
            _fighter = GetComponent<Fighter>();
            _stats = GetComponent<Stats>();
            _mover = GetComponent<Mover>();
            _player = GameObject.FindWithTag("Player");

            _guardPosition = transform.position;
        }

        private void Update()
        {
            if (_stats.IsDead()) return;
               
            if (InAttackRangeOfPlayer() && _fighter.CanAttack(_player))
            {
                _fighter.Attack(_player);
            }
            else
            {
                _mover.StartMoveAction(_guardPosition);
            }
        }

        public bool InAttackRangeOfPlayer()
        {
            return Vector3.Distance(_player.transform.position, transform.position) < chaseDistance; 
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, chaseDistance);
        }
    }

}