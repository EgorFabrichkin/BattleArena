using UnityEngine;

namespace GameCore.Players.Inputs
{
    public interface IPlayerInput
    {
        public Vector3 Direction();

        public Vector3 LookDirection();
        
        public bool Shoot();

        public bool Ultimate();
    }
}