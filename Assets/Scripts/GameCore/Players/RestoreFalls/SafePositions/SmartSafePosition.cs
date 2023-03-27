using System.Collections;
using UnityEngine;

namespace GameCore.Players.RestoreFalls.SafePositions
{
    public class SmartSafePosition : SafePositionBehavior
    {
        [SerializeField] private float updateDelay = 0.3f;
        [Header("Debug")][SerializeField] private Vector3 safePose;

        public override Vector3 Pose() => safePose;

        private IEnumerator Start()
        {
            safePose = transform.position;
            while (this)
            {
                var list = Physics.RaycastAll(
                    new Ray(
                        transform.position,
                        Vector3.down
                    )
                )!;

                foreach (var hit in list)
                {
                    if (hit.collider!.CompareTag("Ground"))
                    {
                        var position = hit.point;
                        position.y = 0.5f;
                        safePose = position;
                    }
                }

                yield return new WaitForSeconds(updateDelay);
            }
        }
    }
}