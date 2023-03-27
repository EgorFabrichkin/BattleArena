using GameCore.Enemies;
using UnityEngine;
using UnityEngine.Events;

namespace GameCore.Weapons.Projectiles
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Collider))]
    public class Projectile : MonoBehaviour
    {
        public UnityEvent OnHit = new();
        
        [SerializeField] private int damage;
        [SerializeField] private float speed;
        [SerializeField] private float lifeTime;
        
        private new Rigidbody rigidbody = null!;
        
        public Rigidbody Rigidbody => rigidbody ??= GetComponent<Rigidbody>()!;
        
        public float Speed => speed;
        
        public float LifeTime => lifeTime;

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.TryGetComponent(out EnemyBase target))
            {
                OnHit.Invoke();
                target.Apply(damage);
            }
        }
    }
}