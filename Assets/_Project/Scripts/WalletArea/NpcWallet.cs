using UnityEngine;

namespace _Project.Scripts.WalletArea
{
    public class NpcWallet : IWallet
    {
        private int Coins { get; set; }
        
        private readonly Transform _creator;
        private float Flip => _creator.localScale.x >= 0 ? 1 : -1;

        public NpcWallet(Transform creator)
        {
            _creator = creator;
        }
        
        public bool AddCurrency(int lootType)
        {
            if (lootType == 1) return false;
            Coins++;
            return true;
        }
        
        public bool ThrowCurrency()
        {
            if (Coins <= 0) return false;
            Coins--;
            var offset = PlayerWallet.ThrowOffset;
            var pos = (Vector2)_creator.position + new Vector2(offset.x * Flip, offset.y);
            PlayerWallet.SpawnLoot(0, pos, PlayerWallet.ParentThrow, Flip);
            return true;
        }
    }
}