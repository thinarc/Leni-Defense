using _Project.Scripts.Input;
using _Project.Scripts.WalletArea;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Abilities.InteractLoot.LootHandlers
{
    public class PlayerInteractLootAbility : IInteractLootAbility, IInitializable, ITickable
    {
        private readonly IWallet _wallet;
        private readonly InputManager _input;
        private readonly float _interactDelay;
        
        private bool _interacted;
        private float _delay;
        
        public PlayerInteractLootAbility(PlayerWallet wallet, InputManager input, float interactDelay)
        {
            _wallet = wallet;
            _input = input;
            _interactDelay = interactDelay;
        }

        public void Initialize()
        {
            _input.GetAllInput("Interact", context => _interacted = context.performed);
        }

        public void Tick()
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