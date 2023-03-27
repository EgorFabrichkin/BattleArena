using System;
using GameCore.Enemies;
using UniRx;
using UnityEngine;
using UnityEngine.Events;

namespace GameCore.Players
{
    [Serializable]
    public class PlayerModel
    {
        public UnityEvent OnDie = new();
        
        public float speed;
        
        [SerializeField] private IntReactiveProperty health = new (100);
        [SerializeField] private IntReactiveProperty power = new (50);
        
        public IReadOnlyReactiveProperty<int> RenderHealth() => health;
        public IReadOnlyReactiveProperty<int> RenderPower() => power;

        public void TakeDamage(int damage)
        {
            health.Value -= damage;
            
            if (health.Value <= 0)
            {
                OnDie.Invoke();
                health.Value = 0;
            }
        }

        public void TakeMagicDamage(int magicDamage)
        {
            power.Value -= magicDamage;
            
            if (power.Value <= 0)
            {
                power.Value = 0;
            }
        }

        public void TryCastUltimate(bool value, IReadOnlyReactiveCollection<EnemyBase> actualEnemies)
        {
            if (!value) return;
            
            if (power.Value == 100)
            {
                foreach (var enemy in actualEnemies)
                {
                    enemy.OnDie.Invoke();
                }

                power.Value = 0;
            }
        }

        public void UpdatePower(int value)
        {
            power.Value += value;
            if (power.Value >= 100)
            {
                power.Value = 100;
            }
        }
    }
}