using UnityEngine;

namespace GameCore.Enemies.SlowEnemies.BlueEnemiesProjectiles
{
    [RequireComponent(typeof(Rigidbody))]
    public class EnemyProjectile : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private int damage = 25;

        private new Rigidbody rigidbody = null!;
       
        private Vector3 targetPosition;
        private bool isTracking;

        private void Start() => rigidbody = GetComponent<Rigidbody>()!;

        public void GetTarget(Vector3 target) => targetPosition = target;

        public int Damage() => damage;
        
        private void FixedUpdate() => FollowPlayer();

        private void FollowPlayer() => rigidbody.velocity = 
            (targetPosition - transform.position).normalized * (speed * Time.fixedDeltaTime);
    }
}