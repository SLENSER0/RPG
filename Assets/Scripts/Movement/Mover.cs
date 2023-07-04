using RPG.Core;
using RPG.LevelStats;
using UnityEngine;
using UnityEngine.AI;


namespace RPG.Movement
{
    public class Mover : MonoBehaviour, IAction
    {
        [SerializeField] private Transform target;
        private BoxCollider _boxCollider;
        private NavMeshAgent _navMeshAgent;
        private Stats _stats;

        private void Start()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _stats = GetComponent<Stats>();
            _boxCollider = GetComponent<BoxCollider>();
        }

        // Update is called once per frame
        void Update()
        {
            if (_boxCollider != null)
            {
                _boxCollider.enabled = !_stats.IsDead();
            }
            _navMeshAgent.enabled = !_stats.IsDead();
            
            UpdateAnimator();
            
        }
        
        public void StartMoveAction(Vector3 destination)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            MoveTo(destination);
        }
    
        public void MoveTo(Vector3 destination)
        {
            _navMeshAgent.destination = destination;
            _navMeshAgent.isStopped = false;
        }

        public void Cancel()
        {
            _navMeshAgent.isStopped = true;
        }
        

        private void UpdateAnimator()
        {
            Vector3 velocity = _navMeshAgent.velocity;
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);
            float speed = localVelocity.z;
            GetComponent<Animator>().SetFloat("forwardSpeed", speed);
        }
        
    }
}
