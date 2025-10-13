using _Project.Scripts.Input;
using _Project.Scripts.WalletArea;
using UnityEngine;

namespace _Project.Scripts.Abilities.EntityInteractResource
{
    public class PlayerInteractResourceAbility : IInteractResourceAbility
    {
        private readonly IWallet _wallet;
        private bool _pressed;
        private float _delay = 0.3f;
        
        public PlayerInteractResourceAbility(PlayerWallet wallet)
        {
            _wallet = wallet;
            InputManager.Instance?.GetAllInput("Interact", context => _pressed = context.performed);
        }

        public void UpdateManually()
        {
            if (_pressed && _delay <= 0f)
            {
                OnInteract();
                _delay = 0.3f;
            }
            else if (!_pressed) _delay = 0f;
            _delay -= Time.deltaTime;
        }
        
        public void OnResource(GameObject res) => PickupResource(res);
        private void PickupResource(GameObject resource)
        {
            _wallet.AddResource(resource.CompareTag("Coin") ? ResourceType.Coin : ResourceType.Ruby);
            Object.Destroy(resource);
        }

        public void OnInteract()
        {
            // _wallet.SpendResource(ResourceType.Coin); тут можно реализовать стратегию как второе поведение
            _wallet.ThrowResource();
        }
    }
}