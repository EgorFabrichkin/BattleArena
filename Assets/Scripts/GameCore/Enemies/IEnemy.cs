using GameCore.Players;

namespace GameCore.Enemies
{
    public interface IEnemy
    {
        public void GetPlayer(Player player);
        
        public void Attack();
    }
}