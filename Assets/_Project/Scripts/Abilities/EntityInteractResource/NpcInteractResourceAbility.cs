using _Project.Scripts.WalletArea;
using UnityEngine;

namespace _Project.Scripts.Abilities.EntityInteractResource
{
    public class NpcInteractResourceAbility : IInteractResourceAbility
    {
        private readonly IWallet _wallet;
        private bool _pressed;
        private float _delay = 0.2f;

        public NpcInteractResourceAbility(Transform creator)
        {
            _wallet = new NpcWallet(creator);
        }
        
        public void UpdateManually()
        {
            if (_pressed && _delay <= 0f)
            {
                _wallet.ThrowResource();
                _delay = 0.2f;
            }
            else if (!_pressed) _delay = 0f;
            _delay -= Time.deltaTime;
        }
        
        public void OnResource(GameObject res) => PickupResource(res);
        private void PickupResource(GameObject resource)
        {
            if (_wallet.AddResource(resource.CompareTag("Coin") ? ResourceType.Coin : ResourceType.Ruby))
                Object.Destroy(resource);
        }

        public void OnInteract()
        {
            _pressed = true;
            _delay = 0.6f;
        }

        public void OnCancelInteract()
        {
            _pressed = false;
        }
    }
}