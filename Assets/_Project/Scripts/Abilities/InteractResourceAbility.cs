using UnityEngine;
using Sirenix.OdinInspector;
using _Project.Scripts.WalletArea;
using _Project.Scripts.Abilities.EntityInteractResource;

namespace _Project.Scripts.Abilities
{
    public class InteractResourceAbility : MonoBehaviour
    {
        [SerializeField] private ControlType controlType;
        [ShowIf("controlType", ControlType.Player), SerializeField, Space(5)] private PlayerWallet playerWallet;
        
        private IInteractResourceAbility _interact;

        private void Start() => InitializeHandler();
        [Button("Reinitialize Handler When The Type IS Changed"), PropertySpace(5)] private void InitializeHandler()
        {
            if (controlType == ControlType.Player) _interact = new PlayerInteractResourceAbility(playerWallet);
            else if (controlType == ControlType.Npc) _interact = new NpcInteractResourceAbility(transform);
            else _interact = new EnemyInteractResourceAbility(this.transform);
        }
        
        private void Update()
        {
            if (controlType == ControlType.Player)
                (_interact as PlayerInteractResourceAbility)?.UpdateManually();
            if (controlType == ControlType.Npc)
                (_interact as NpcInteractResourceAbility)?.UpdateManually();
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Coin") || other.gameObject.CompareTag("Ruby"))
                _interact.OnResource(other.gameObject);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (controlType == ControlType.Npc && other.CompareTag("Player"))
                _interact.OnInteract();
        }
        private void OnTriggerExit2D(Collider2D other)
        {
            if (controlType == ControlType.Npc && other.CompareTag("Player"))
                _interact.OnCancelInteract();
        }
    }
    
    public interface IInteractResourceAbility
    {
        public void OnResource(GameObject res);
        public void OnInteract();
        public void OnCancelInteract() { }
    }
}