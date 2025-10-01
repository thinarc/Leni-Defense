using _Project.Scripts.Brains;
using UnityEngine;

namespace _Project.Scripts.Movement
{
    public class MovementController : MonoBehaviour
    {
        [SerializeField] private MovementStats stats;
        private Rigidbody2D _rb;
        private IMoveBrain _moveBrain;
        private Vector2 _frameVelocity;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _moveBrain = GetComponent<IMoveBrain>();
            
            _frameVelocity.y = stats.groundingForce;
        }

        private void FixedUpdate()
        {
            HandleDirection();
            ApplyMovement();
        }

        #region Horizontal

        private void HandleDirection()
        {
            float sprintModifier = _moveBrain.IsSprinting ? stats.sprintModifier : 1f;
            
            if (_moveBrain.Horizontal == 0)
            {
                var deceleration = stats.groundDeceleration;
                _frameVelocity.x = Mathf.MoveTowards(_frameVelocity.x, 0, deceleration * Time.fixedDeltaTime);
            }
            else
            {
                var acceleration = stats.acceleration;
                _frameVelocity.x = Mathf.MoveTowards(_frameVelocity.x, _moveBrain.Horizontal * stats.maxSpeed * sprintModifier, acceleration * Time.fixedDeltaTime);
            }
        }

        #endregion
        
        private void ApplyMovement() => _rb.linearVelocity = _frameVelocity;
    }
}