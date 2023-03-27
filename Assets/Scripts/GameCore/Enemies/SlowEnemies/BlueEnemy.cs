using System.Collections;
using System.Collections.Generic;
using GameCore.Enemies.SlowEnemies.BlueEnemiesProjectiles;
using UnityEngine;
using Utils;

namespace GameCore.Enemies.SlowEnemies
{
    public class BlueEnemy : EnemyBase
    {
        //Переменные врага для удобства найстройки можно вынести в ScriptableObject
        [SerializeField] private int projectilePerShoot;
        [SerializeField] private float shootDelay;
        
        [SerializeField] private EnemyProjectile projectilePrefab = null!;
        [SerializeField] private Transform shootPoint = null!;

        private List<EnemyProjectile> actualProjectiles = new ();

        private bool isReadyToAttack;

        private void Awake()
        {
            projectilePrefab.EnsureNotNull("EnemyProjectile prefab not specified");
            shootPoint.EnsureNotNull("ShootPoint not specified");
            isReadyToAttack = true;
        }

        private void FixedUpdate()
        {
            TryLookAtPlayer();
            TryAttack();
            TryGetTarget();
        }
        
        private void TryLookAtPlayer()
        {
            if (origin == null) return;
            transform.LookAt(origin.transform.position);
        }

        private void TryAttack()
        {
            if (!isReadyToAttack) return;
            
            for (var i = 0; i < projectilePerShoot; i++)
            {
                Attack();
                isReadyToAttack = false;
                StartCoroutine(AttackDelay());
            }
        }

        public override void Attack()
        {
            //Можно использовать ObjectPool для улучшения оптимизации
            var newProjectile = Instantiate(projectilePrefab, shootPoint);
            actualProjectiles.Add(newProjectile);
        }

        private IEnumerator AttackDelay()
        {
            yield return new WaitForSeconds(shootDelay);
            isReadyToAttack = true;
        }

        private void TryGetTarget()
        {
            if (origin == null) return;
            actualProjectiles.ForEach(p => p.GetTarget(origin.transform.position));
        }
    }
}