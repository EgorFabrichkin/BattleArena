using UnityEngine;
using Utils;

namespace GameCore.Players.Inputs
{
    public class LookDirection : MonoBehaviour
    {
        [SerializeField] private float lookSensitivity;
        [SerializeField] private float minFov = 90f;
        [SerializeField] private float maxFov = 90f;
        [SerializeField] private Transform player = null!;

        private readonly IPlayerInput input = new KeyboardInput();
        
        private float xRotation, mouseX, mouseY;

        private void Awake() => player.EnsureNotNull("player not specified");

        private void Update()
        {
            mouseX = input.LookDirection().x * lookSensitivity * Time.deltaTime;
            mouseY = input.LookDirection().y * lookSensitivity * Time.deltaTime;
        }
        private void FixedUpdate()
        { 
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -minFov, maxFov);
            transform.localRotation = Quaternion.Euler(xRotation,0,0);
            player.Rotate(Vector3.up * mouseX);
        }
    }
}