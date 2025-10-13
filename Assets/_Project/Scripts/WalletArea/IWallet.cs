namespace _Project.Scripts.WalletArea
{
    public interface IWallet
    {
        public bool AddResource(ResourceType resourceType);
        public bool ThrowResource();
    }
}