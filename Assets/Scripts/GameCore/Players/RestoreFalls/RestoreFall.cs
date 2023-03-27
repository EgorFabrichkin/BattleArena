using System.Collections;
using GameCore.Players.RestoreFalls.SafePositions;
using UnityEngine;
using UnityEngine.Events;
using Utils;

namespace GameCore.Players.RestoreFalls
{
    [RequireComponent(typeof(Player))]
    public class RestoreFall : MonoBehaviour
    {
        public static readonly UnityEvent OnRestored = new();
        
        [SerializeField] private SafePositionBehavior safePosition = null!;
        [SerializeField] private float fallYTrigger = -2;
        [SerializeField] private float updateDelay = 0.3f;
        
        private IEnumerator Start()
        {
            safePosition.EnsureNotNull("Safe position not found");
            
            while (this)
            {
                if (transform.position.y < fallYTrigger)
                {
                    transform.position = safePosition.Pose();
                    OnRestored.Invoke();
                }

                yield return new WaitForSeconds(updateDelay);
            }
        }
    }
}