using UnityEngine;

namespace _Project.Scripts.Brains
{
    public class NpcBrain : MonoBehaviour, IMoveBrain
    {
        public float Horizontal => 0;
        public bool IsSprinting => false;
    }
}