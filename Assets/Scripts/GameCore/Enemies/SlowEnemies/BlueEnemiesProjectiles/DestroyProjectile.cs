using GameCore.Players;
using GameCore.Players.RestoreFalls;
using UnityEngine;

namespace GameCore.Enemies.SlowEnemies.BlueEnemiesProjectiles
{
    [RequireComponent(typeof(EnemyProjectile))]
    public class DestroyProjectile : MonoBehaviour
    {
        [SerializeField] private float delay;
        private int damage;
        private void Awake()
        {
            damage = GetComponent<EnemyProjectile>()!.Damage();
            RestoreFall.OnRestored.AddListener(() => Destroy(gameObject, delay));
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.TryGetComponent(out Player player))
            {
                player.TakeMagicDamage(damage);
                Destroy(gameObject);
            }
        }
    }
}