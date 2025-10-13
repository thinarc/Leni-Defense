using UnityEngine;

namespace _Project.Scripts.WalletArea
{
    public class NpcWallet : IWallet
    {
        public int Coins { get; set; }
        
        private readonly Transform _creator;
        private float Flip => _creator.localScale.x >= 0 ? 1 : -1;

        public NpcWallet(Transform creator)
        {
            _creator = creator;
        }
        
        public bool AddResource(ResourceType resourceType)
        {
            if (resourceType == ResourceType.Ruby) return false;
            Coins++;
            return true;
        }
        
        public bool ThrowResource()
        {
            if (Coins > 0)
            {
                Coins--;
                Vector2 pos = (Vector2)_creator.position + new Vector2(PlayerWallet.SpawnOffset.x * Flip, PlayerWallet.SpawnOffset.y);
                PlayerWallet.SpawnResource(ResourceType.Coin, pos, PlayerWallet.ParentSpawn, Flip);
                return true;
            }
            return false;
        }
    }
}