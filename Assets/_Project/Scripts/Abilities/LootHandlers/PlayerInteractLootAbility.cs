using _Project.Scripts.Input;
using _Project.Scripts.WalletArea;
using UnityEngine;

namespace _Project.Scripts.Abilities.LootHandlers
{
    public class PlayerInteractLootAbility : IInteractLootAbility
    {
        private readonly IWallet _wallet;
        private readonly float _interactDelay;
        
        private bool _interacted;
        private float _delay;
        
        public PlayerInteractLootAbility(PlayerWallet wallet, float interactDelay)
        {
            _wallet = wallet;
            _interactDelay = interactDelay;
            InputManager.Instance?.GetAllInput("Interact", context => _interacted = context.performed);
        }

        public void ManualUpdate()
        {
            if (_interacted && _delay <= 0f)
            {
                OnInteract();
                _delay = _interactDelay;
            }
            _delay -= Time.deltaTime;
        }
        
        public void OnCaughtLoot(GameObject loot)
        {
            _wallet.AddCurrency(loot.CompareTag("Coin") ? 0 : 1);
            Object.Destroy(loot);
        }

        public void OnInteract() => _wallet.ThrowCurrency();
    }
}