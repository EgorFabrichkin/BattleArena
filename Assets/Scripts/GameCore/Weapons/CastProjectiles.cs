using System.Collections;
using GameCore.Players;
using GameCore.Weapons.Projectiles;
using UnityEngine;
using Utils;

namespace GameCore.Weapons
{
    [RequireComponent(typeof(Player))]
    public class CastProjectiles : MonoBehaviour
    {
        [SerializeField] private Projectile projectilePrefab = null!;
        [SerializeField] private int countProjectile;
        [SerializeField] private float delay;
        [SerializeField] private float maxDistance;

        private bool isReadyToAttack;
        private new Camera camera = null!;
        private ObjectPool<Projectile> objectPool = null!;

        private void Awake()
        {
            projectilePrefab.EnsureNotNull("Projectile prefab not specified");
            objectPool = new ObjectPool<Projectile>(projectilePrefab, countProjectile);
            camera = Camera.main!;
            isReadyToAttack = true;
        }

        public void TryAttack()
        {
            if (!isReadyToAttack) return;
            
            StartCoroutine(Attack()); 
            isReadyToAttack = false;
            StartCoroutine(AttackDelay());
        }

        private IEnumerator Attack()
        {
            var ray = camera.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f, 0f));
            
            if (Physics.Raycast(ray, out var hit, maxDistance))
            {
                if(hit.collider != null)
                {
                    var newProjectile = objectPool.TryGetObject();
                    var newProjectileRigidbody = newProjectile.Rigidbody;
                    
                    newProjectileRigidbody.isKinematic = false;
                    newProjectile.transform.SetPositionAndRotation(transform.position, Quaternion.identity);
                    
                    newProjectileRigidbody.velocity = ray.direction * newProjectile.Speed;
                    
                    newProjectile.OnHit.AddListener(ReturnProjectile);
                    
                    yield return new WaitForSeconds(newProjectile.LifeTime);
                    
                    ReturnProjectile();

                    void ReturnProjectile()
                    {
                        newProjectileRigidbody!.isKinematic = true;
                        objectPool.ReturnToPool(newProjectile!);
                    }
                }
            }
        }

        private IEnumerator AttackDelay()
        {
            yield return new WaitForSeconds(delay);
            isReadyToAttack = true;
        }
    }
}