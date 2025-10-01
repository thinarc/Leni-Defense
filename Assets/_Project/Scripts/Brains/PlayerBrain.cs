using _Project.Scripts.Input;
using UnityEngine;

namespace _Project.Scripts.Brains
{
    public class PlayerBrain : MonoBehaviour, IMoveBrain
    {
        private void Awake()
        {
            Debug.LogWarning("The input system has been found manually. In future, configure the start function calls in the correct order."
                + (InputManager.Instance == null ? " InputManager not found." : " InputManager found."));
            // InputManager.Instance?.GetInput("Horizontal", (context => { Horizontal = context.ReadValue<float>(); }));
            
            FindAnyObjectByType<InputManager>()?.GetInput("Horizontal", (context => { Horizontal = context.ReadValue<Vector2>().x; }));
            FindAnyObjectByType<InputManager>()?.GetAllInput("Sprint", (context => { IsSprinting = context.performed; }));
        }
        
        // Interface
        public float Horizontal { get; private set; }
        public bool IsSprinting { get; private set; }
    }
}