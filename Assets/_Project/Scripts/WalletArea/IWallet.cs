namespace _Project.Scripts.WalletArea
{
    public interface IWallet
    {
        public bool AddCurrency(int lootType);
        public bool ThrowCurrency();
    }
}