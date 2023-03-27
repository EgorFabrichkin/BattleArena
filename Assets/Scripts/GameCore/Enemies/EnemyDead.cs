using UnityEngine;

namespace GameCore.Enemies
{
    [RequireComponent(typeof(EnemyBase))]
    public class EnemyDead : MonoBehaviour
    {
        private void Awake()
        {
            GetComponent<EnemyBase>()!.OnDie.AddListener(() => Destroy(gameObject));
        }
    }
}