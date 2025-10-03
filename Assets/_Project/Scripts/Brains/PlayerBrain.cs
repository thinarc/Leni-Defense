using _Project.Scripts.Input;
using UnityEngine;

namespace _Project.Scripts.Brains
{
    public class PlayerBrain : MonoBehaviour, IMoveBrain
    {
        private void Start()
        {
            InputManager.Instance?.GetInput("Horizontal", (context => { Horizontal = context.ReadValue<Vector2>().x; }));
            InputManager.Instance?.GetAllInput("Sprint", (context => { IsSprinting = context.performed; }));
        }
        
        // Interface
        public float Horizontal { get; private set; }
        public bool IsSprinting { get; private set; }
    }
}