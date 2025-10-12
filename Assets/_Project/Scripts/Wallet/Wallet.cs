using UnityEngine;

namespace _Project.Scripts.Wallet
{
    public class Wallet : MonoBehaviour
    {
        [SerializeField] private WalletPhysics physics;
        
        public int Coins { get; private set; }
        public int Rubies { get; private set; }
        
        public void AddResource(ResourceType resourceType)
        {
            if (resourceType == ResourceType.Coin) Coins++;
            else Rubies++;
            physics.RegisterResource(resourceType);
        }

        public bool SpendResource(ResourceType resourceType)
        {
            if (!physics.TryFindResource(resourceType)) return false;
            if (resourceType == ResourceType.Coin) Coins--;
            else Rubies--;
            return true;
        }

        [ContextMenu("AddCoin")]
        private void AddResourceCoin() => AddResource(ResourceType.Coin);
        [ContextMenu("AddRuby")]
        private void AddResourceRuby() => AddResource(ResourceType.Ruby);
    }
}

public enum ResourceType
{
    Coin,
    Ruby
}