using System;
using GameCore.Players;
using GameCore.Weapons.Targets;
using UnityEngine;
using UnityEngine.Events;

namespace GameCore.Enemies
{
    [RequireComponent(typeof(Collider))]
    public abstract class EnemyBase : MonoBehaviour, IEnemy, ITarget
    {
        //По ТЗ игрок ваншотит оба вида врага, поэтому не совсем понятно, для чего им разный уровень здоровья :) 
        public UnityEvent OnDie = new();

        [SerializeField] private int health;
        [SerializeField] private int enemyPower;

        protected Player origin = null!;

        public void GetPlayer(Player player) => origin = player;
        
        public abstract void Attack();

        public void Apply(int playerDamage)
        {
            {
                health -= playerDamage;
                if (health <= 0)
                {
                    origin.GetPowerForKill(enemyPower);
                    OnDie.Invoke();
                }
            }   
        }
    }
}