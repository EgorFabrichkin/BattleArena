using UnityEngine;

namespace GameCore.Players.Inputs
{
    public class KeyboardInput : IPlayerInput
    {
        public Vector3 Direction()
        {
            return new Vector3(
                Input.GetAxisRaw("Horizontal"),
                0,
                Input.GetAxisRaw("Vertical")
            );
        }

        public Vector3 LookDirection()
        {
            return new Vector3(
                Input.GetAxis("Mouse X"),
                Input.GetAxis("Mouse Y"),
                0
            );
        }
        
        public bool Shoot() => Input.GetMouseButtonDown(0) || Input.GetMouseButton(0);

        public bool Ultimate() => Input.GetKeyDown(KeyCode.Space);
    }
}