using _Project.Scripts.Stats;
using UnityEngine;

namespace _Project.Scripts.Player
{
    public class PlayerController : MonoBehaviour, IPlayerController
    {
        [SerializeField] private MovementStats stats;
        private Rigidbody2D _rb;
        private CapsuleCollider2D _col;
        private FrameInput _frameInput;
        private Vector2 _frameVelocity;

        #region Interface

        public float Direction => _frameInput.Horizontal;

        #endregion

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            GatherInput();
        }

        private void GatherInput()
        {
            _frameInput = new FrameInput
            {
                Horizontal = 1
            };

            if (stats.SnapInput)
                _frameInput.Horizontal = Mathf.Abs(_frameInput.Horizontal) < stats.HorizontalDeadZoneThreshold ? 0 : Mathf.Sign(_frameInput.Horizontal);
        }

        private void FixedUpdate()
        {
            HandleDirection();
            ApplyMovement();
        }

        #region Horizontal

        private void HandleDirection()
        {
            if (_frameInput.Horizontal == 0)
            {
                var deceleration = stats.GroundDeceleration;
                _frameVelocity.x = Mathf.MoveTowards(_frameVelocity.x, 0, deceleration * Time.fixedDeltaTime);
            }
            else
            {
                var acceleration = stats.Acceleration;
                _frameVelocity.x = Mathf.MoveTowards(_frameVelocity.x, _frameInput.Horizontal * stats.MaxSpeed, acceleration * Time.fixedDeltaTime);
            }
        }

        #endregion
        
        private void ApplyMovement() => _rb.linearVelocity = _frameVelocity;
    }

    public struct FrameInput
    {
        public float Horizontal;
    }

    public interface IPlayerController
    {
        public float Direction { get; }
    }
}