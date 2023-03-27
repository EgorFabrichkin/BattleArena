using UnityEngine;

namespace GameCore.Weapons.Targets
{
    public class SimpleTarget : MonoBehaviour, ITarget
    {
        [SerializeField] private int health = 100;
        
        public void Apply(int playerDamage)
        {
            health -= playerDamage;
            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}