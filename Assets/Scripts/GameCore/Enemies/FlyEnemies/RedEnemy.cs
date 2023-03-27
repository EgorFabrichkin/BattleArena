using System.Collections;
using UnityEngine;

namespace GameCore.Enemies.FlyEnemies
{
    [RequireComponent(typeof(Rigidbody))]
    public class RedEnemy : EnemyBase
    {
        //Переменные врага для удобства найстройки можно вынести в ScriptableObject 
        [SerializeField] private float upSpeed;         
        [SerializeField] private float upDistance;       
        [SerializeField] private float pauseTime;      
        [SerializeField] private float attackSpeed;     
        [SerializeField] private int damage = 15;

        private new Rigidbody rigidbody = null!;
        
        private bool isPaused;      
        
        private void Start() => rigidbody = GetComponent<Rigidbody>();

        private void FixedUpdate()
        {
            if (!isPaused)
            {
                StartCoroutine(Move());
                return;
            }
            Attack();
        }

        private IEnumerator Move()
        {
            if (transform.position.y < upDistance)
            {
                rigidbody.velocity = new Vector3(0, upSpeed, 0);
            }
            else
            {
                isPaused = true;
                rigidbody.velocity = Vector3.zero;
                yield return new WaitForSeconds(pauseTime);
            }
        }

        public override void Attack() => rigidbody.velocity = 
            (origin.transform.position - transform.position).normalized
            * (attackSpeed * Time.fixedDeltaTime);

        private void OnCollisionEnter(Collision col)
        {
            if (col.collider.CompareTag("Player"))
            {
                origin.TakeDamage(damage);
                Destroy(gameObject);
            }
        }
    }
}