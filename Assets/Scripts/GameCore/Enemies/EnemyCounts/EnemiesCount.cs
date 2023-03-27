using UniRx;
using UnityEngine;

namespace GameCore.Enemies.EnemyCounts
{
    public class EnemiesCount : MonoBehaviour
    {
        private IntReactiveProperty count = new();
        private IReadOnlyReactiveCollection<EnemyBase> actualEnemies = new ReactiveCollection<EnemyBase>();

        private void Update()
        {
            foreach (var enemy in actualEnemies)
            {
                enemy.OnDie.AddListener(() => count.Value++ );
            }
        }

        public IReadOnlyReactiveProperty<int> Count => count;
        
        public void GetActualEnemies(IReadOnlyReactiveCollection<EnemyBase> list) => actualEnemies = list;
    }
}