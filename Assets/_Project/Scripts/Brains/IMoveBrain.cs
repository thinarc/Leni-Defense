namespace _Project.Scripts.Brains
{
    public interface IMoveBrain
    {
        public float Horizontal { get; }
        public bool IsSprinting { get; }
    }
}