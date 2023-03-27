using System.Collections;
using GameCore.Enemies.FlyEnemies;
using GameCore.Enemies.SlowEnemies;
using UniRx;
using UnityEngine;
using Utils;
using Random = UnityEngine.Random;

namespace GameCore.Enemies
{
    public class SpawnEnemies : MonoBehaviour
    {
        [SerializeField] private BlueEnemy blueEnemyPrefabs = null!;
        [SerializeField] private RedEnemy redEnemyPrefabs = null!;
        //Переменные ниже для гибкости найстройки можно вынести в DATA/LevelSettings
        [SerializeField] private int minSpawnTimeRate;
        [SerializeField] private int maxSpawnTimeRate;

        private IReactiveCollection<EnemyBase> actualEnemies = new ReactiveCollection<EnemyBase>();
        private new Renderer renderer = null!;

        private int currentSpawnTime;

        private void Awake()
        {
            blueEnemyPrefabs.EnsureNotNull("BlueEnemy prefabs not specified");
            redEnemyPrefabs.EnsureNotNull("RedEnemy prefabs not specified");
            renderer = GetComponent<Renderer>()!;
        }

        private IEnumerator Start()
        {
            currentSpawnTime = maxSpawnTimeRate;

            while (minSpawnTimeRate <= currentSpawnTime)
            {
                SpawnEnemy();
                yield return new WaitForSeconds(currentSpawnTime);
                currentSpawnTime--;
            }

            while (true)
            {
                yield return new WaitForSeconds(minSpawnTimeRate);
                SpawnEnemy();
            }
        }

        private void SpawnEnemy()
        {
            EnemyBase prefab = Random.Range(0, 4) == 0 ? blueEnemyPrefabs : redEnemyPrefabs;
            
            var spawnPosition = transform.position + Random.insideUnitSphere * CalculatePlaneSize();
            spawnPosition.y = 0.5f;
            
            //Можно использовать ObjectPool для улучшения оптимизации
            var newEnemy = Instantiate(prefab, spawnPosition, Quaternion.identity);
            actualEnemies.Add(newEnemy);
        }
 
        public IReadOnlyReactiveCollection<EnemyBase> ActualEnemies() => actualEnemies;

        private float CalculatePlaneSize() => renderer.bounds.size.x / 2;
    }
}