namespace _Project.Scripts.Brains
{
    public abstract class MoveBrain
    {
        public virtual float Horizontal { get; protected set; }
        public virtual bool IsSprinting { get; protected set; }
    }
}