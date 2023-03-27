using GameCore.Enemies;
using GameCore.Players.Inputs;
using GameCore.Weapons;
using UniRx;
using UnityEngine;
using UnityEngine.Events;

namespace GameCore.Players
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Collider))]
    public class Player : MonoBehaviour
    {
        [SerializeField] private PlayerModel player = new();
        private readonly IPlayerInput input = new KeyboardInput();
        private new Rigidbody rigidbody = null!;
        private CastProjectiles castProjectiles = null!;
        private IReadOnlyReactiveCollection<EnemyBase> actualEnemies = null!;
        
        private Vector3 direction;
        private bool isAttack;

        public void GetActualEnemies(IReadOnlyReactiveCollection<EnemyBase> enemies) => actualEnemies = enemies;

        private void Awake()
        {
            rigidbody = GetComponent<Rigidbody>()!;
            castProjectiles = GetComponent<CastProjectiles>()!;
            player.OnDie.AddListener(() => OnPlayerDead());
        }

        private void Update()
        {
            direction = transform.right * input.Direction().x + transform.forward * input.Direction().z;
            isAttack = input.Shoot();
            
            player.TryCastUltimate(input.Ultimate(), actualEnemies);
        }

        private void FixedUpdate()
        {
            Move();
            TryAttack(isAttack);
        }

        private void Move() => rigidbody.position +=((direction * (Time.fixedDeltaTime * player.speed)));

        private void TryAttack(bool value)
        {
            if (!value) return;
            castProjectiles.TryAttack();
        }

        public IReadOnlyReactiveProperty<int> RenderHealth() => player.RenderHealth();
        
        public IReadOnlyReactiveProperty<int> RenderPower() => player.RenderPower();
        
        public void TakeDamage(int damage) => player.TakeDamage(damage);

        public void TakeMagicDamage(int magicDamage) => player.TakeMagicDamage(magicDamage);

        public void GetPowerForKill(int enemyPower) => player.UpdatePower(enemyPower);

        public virtual UnityEvent OnPlayerDead() => player.OnDie;
        private void OnDestroy() => player.OnDie.RemoveListener(() => OnPlayerDead());
    }
}