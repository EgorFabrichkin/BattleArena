using UnityEngine;

namespace GameCore.Players.RestoreFalls.SafePositions
{
    public abstract class SafePositionBehavior : MonoBehaviour, ISafePosition
    {
        public abstract Vector3 Pose();
    }
}