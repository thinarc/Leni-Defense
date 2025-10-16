using _Project.Scripts.WalletArea;
using UnityEngine;

namespace _Project.Scripts.Abilities.LootHandlers
{
    public class NpcInteractLootAbility : IInteractLootAbility
    {
        private readonly IWallet _wallet;
        private readonly float _interactDelay;
        private readonly Collider2D _collider;
        
        private bool _interacted;
        private float _delay;

        public NpcInteractLootAbility(Transform creator, float interactDelay, Collider2D collider)
        {
            _wallet = new NpcWallet(creator);
            _interactDelay = interactDelay;
            _delay = _interactDelay;
            _collider = collider;
        }
        
        public void ManualUpdate()
        {
            if (_interacted && _delay <= 0f)
            {
                _wallet.ThrowCurrency();
                _delay = _interactDelay / 3f;
            }
            _delay -= Time.deltaTime;
        }

        public void OnCaughtLoot(GameObject loot)
        {
            if (_wallet.AddCurrency(loot.CompareTag("Coin") ? 0 : 1))
            {
                Object.Destroy(loot);
                _delay = _interactDelay;
            }
            else Physics2D.IgnoreCollision(_collider, loot.GetComponent<Collider2D>());
        }

        public void OnInteract()
        {
            _interacted = true;
            _delay = _interactDelay;
        }
        public void OnCancelInteract() => _interacted = false;
    }
}