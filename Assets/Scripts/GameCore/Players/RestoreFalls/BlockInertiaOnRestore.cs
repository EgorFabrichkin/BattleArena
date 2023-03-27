using UnityEngine;

namespace GameCore.Players.RestoreFalls
{
    [RequireComponent(typeof(RestoreFall))]
    [RequireComponent(typeof(Rigidbody))]
    public class BlockInertiaOnRestore : MonoBehaviour
    {
        //private RestoreFall restoreFall = null!;
        private new Rigidbody rigidbody = null!;

        private void Awake() 
        {
            //restoreFall = GetComponent<RestoreFall>()!;
            rigidbody = GetComponent<Rigidbody>()!;

            RestoreFall.OnRestored.AddListener(() =>
                {
                    rigidbody.velocity = Vector3.zero;
                    rigidbody.angularVelocity = Vector3.zero;
                }
            );
        }
    }
}