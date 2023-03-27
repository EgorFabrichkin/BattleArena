using GameCore.Enemies;
using GameCore.Enemies.EnemyCounts;
using GameCore.Players;
using UI;
using UnityEngine;
using Utils;

public class EntryPoint : MonoBehaviour
{
    [SerializeField] private Player player = null!;
    [SerializeField] private SpawnEnemies spawnEnemies = null!;
    [SerializeField] private EnemiesCount enemiesCount = null!;
    [SerializeField] private UIFacade uiFacade = null!;
    
    private void Awake()
    {
        player.EnsureNotNull("Player not found");
        spawnEnemies.EnsureNotNull("Spawn enemies not found");
        enemiesCount.EnsureNotNull("EnemiesCount not found");
        uiFacade.EnsureNotNull("UIFacade not found");
    }

    private void Start()
    {
        enemiesCount.GetActualEnemies(spawnEnemies.ActualEnemies());
        player.GetActualEnemies(spawnEnemies.ActualEnemies());
        
        uiFacade.InitRenderValue(player.RenderHealth(), player.RenderPower());
        uiFacade.InitRenderLoseCanvas(enemiesCount.Count);
        uiFacade.OnLose(player.OnPlayerDead());
    }

    private void Update()
    {
        foreach (var enemy in spawnEnemies.ActualEnemies())
        {
            enemy.GetPlayer(player);
        }
    }
}
