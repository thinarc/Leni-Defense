using _Project.Scripts.Brains;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Movement
{
    [RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
    public class MovementController : MonoBehaviour, IInitializable
    {
        [SerializeField] private MovementStats stats;
        private Rigidbody2D _rb;
        private MoveBrain _moveBrain;
        private float _frameVelocityX;
        private bool _isFacingRight;
        
        private bool _initialized;

        [Inject]
        private void Construct(MoveBrain moveBrain) => _moveBrain = moveBrain;

        public void Initialize()
        {
            _rb = GetComponent<Rigidbody2D>();
            
            _isFacingRight = transform.localScale.x >= 0;
            _initialized = true;
        }

        private void FixedUpdate()
        {
            if (!_initialized) return;
            HandleDirection();
            ApplyMovement();
        }

        #region Horizontal

        private void HandleDirection()
        {
            var sprintModifier = _moveBrain.IsSprinting ? stats.sprintModifier : 1f;
            
            if (_moveBrain.Horizontal == 0)
            {
                var deceleration = stats.deceleration;
                _frameVelocityX = Mathf.MoveTowards(_frameVelocityX, 0, deceleration * Time.fixedDeltaTime);
            }
            else
            {
                var acceleration = stats.acceleration;
                _frameVelocityX = Mathf.MoveTowards(_frameVelocityX, _moveBrain.Horizontal * stats.maxSpeed * sprintModifier, acceleration * Time.fixedDeltaTime);
            }
            
            if (_moveBrain.Horizontal > 0 && !_isFacingRight) Flip();
            else if (_moveBrain.Horizontal < 0 && _isFacingRight) Flip();
        }

        private void Flip()
        {
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
            _isFacingRight = !_isFacingRight;
        }

        #endregion
        
        private void ApplyMovement() => _rb.linearVelocityX = _frameVelocityX;
    }
}