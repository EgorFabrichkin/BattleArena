using UnityEngine;
using Utils;

namespace GameCore.Players.RestoreFalls.SafePositions
{
    public class SimpleSafePosition : SafePositionBehavior
    {
        [SerializeField] private Transform defaultSafePosition = null!;

        private void Start()
        {
            defaultSafePosition.EnsureNotNull("Default Safe Position not specified");
        }

        public override Vector3 Pose() => defaultSafePosition.position;
    }
}