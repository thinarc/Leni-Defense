using UnityEngine;

namespace _Project.Scripts.Movement
{
    [CreateAssetMenu]
    public class MovementStats : ScriptableObject
    {
        [Header("MOVEMENT")]
        public float maxSpeed = 14;
        public float sprintModifier = 1.4f;
        public float acceleration = 120;
        public float groundDeceleration = 60;
        [Range(0f, -10f)] public float groundingForce = -1.5f;
    }
}