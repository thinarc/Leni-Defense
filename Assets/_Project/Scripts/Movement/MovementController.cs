using _Project.Scripts.Brains;
using UnityEngine;

namespace _Project.Scripts.Movement
{
    [RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
    public class MovementController : MonoBehaviour
    {
        [SerializeField] private MovementStats stats;
        private Rigidbody2D _rb;
        private IMoveBrain _moveBrain;
        private float _frameVelocityX;
        private bool _isFacingRight;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _moveBrain = GetComponent<IMoveBrain>();
            
            _isFacingRight = transform.localScale.x >= 0;
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