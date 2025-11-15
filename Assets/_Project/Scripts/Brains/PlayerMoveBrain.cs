using _Project.Scripts.Input;
using UnityEngine;

namespace _Project.Scripts.Brains
{
    public class PlayerMoveBrain : MoveBrain
    {
        public PlayerMoveBrain Initialize()
        {
            InputManager.Instance?.GetAllInput("Horizontal", (context => { Horizontal = context.ReadValue<Vector2>().x; }));
            InputManager.Instance?.GetAllInput("Sprint", (context => { IsSprinting = context.performed; }));
            return this;
        }
        
        public override float Horizontal { get; protected set; }
        public override bool IsSprinting { get; protected set; }
    }
}