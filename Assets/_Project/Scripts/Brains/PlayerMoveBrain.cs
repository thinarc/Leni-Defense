using _Project.Scripts.Input;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Brains
{
    public class PlayerMoveBrain : MoveBrain, IInitializable
    {
        private readonly InputManager _input;

        public PlayerMoveBrain(InputManager input) => _input = input;
        
        public void Initialize()
        {
            _input.GetAllInput("Horizontal", (context => { Horizontal = context.ReadValue<Vector2>().x; }));
            _input.GetAllInput("Sprint", (context => { IsSprinting = context.performed; }));
        }
        
        public override float Horizontal { get; protected set; }
        public override bool IsSprinting { get; protected set; }
    }
}