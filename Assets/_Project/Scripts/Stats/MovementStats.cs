using UnityEngine;

namespace _Project.Scripts.Stats
{
    [CreateAssetMenu]
    public class MovementStats : ScriptableObject
    {
        [Header("INPUT")]
        public bool SnapInput = true;
        [Range(0.01f, 0.99f)] public float HorizontalDeadZoneThreshold = 0.1f;

        [Header("MOVEMENT")]
        public float MaxSpeed = 14;
        public float Acceleration = 120;
        public float GroundDeceleration = 60;
        [Range(0f, -10f)] public float GroundingForce = -1.5f;
    }
}