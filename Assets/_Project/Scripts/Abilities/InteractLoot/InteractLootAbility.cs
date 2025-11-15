using _Project.Scripts.Abilities.InteractLoot.LootHandlers;
using _Project.Scripts.WalletArea;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Project.Scripts.Abilities.InteractLoot
{
    public class InteractLootAbility : MonoBehaviour
    {
        private enum ControlType { Player, Npc, Enemy }
        [SerializeField] private ControlType controlType;
        
        [ShowIf("controlType", ControlType.Player), SerializeField, Space(5)] private PlayerWallet playerWallet;
        private float InteractDelay => controlType == ControlType.Player ? 0.27f : 0.6f;
        
        [ShowIf("controlType", ControlType.Npc), SerializeField, Space(5)] private Collider2D npcCollider;
        
        private IInteractLootAbility _handler;

        public void Initialize() => InitializeHandler();
        [Button("Initialize manually"), PropertySpace(5)]
        private void InitializeHandler()
        {
            _handler = controlType switch
            {
                ControlType.Player => new PlayerInteractLootAbility(playerWallet, InteractDelay),
                ControlType.Npc => new NpcInteractLootAbility(transform, InteractDelay, npcCollider),
                ControlType.Enemy => new EnemyInteractLootAbility(transform),
                _ => null
            };
        }
        
        private void Update()
        {
            if (_handler is ITickable tickable)
                tickable.Tick();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Coin") || other.gameObject.CompareTag("Ruby"))
                _handler.OnCaughtLoot(other.gameObject);
            
            if (controlType == ControlType.Npc && other.CompareTag("Player"))
                _handler.OnInteract();
        }
        private void OnTriggerExit2D(Collider2D other)
        {
            if (controlType == ControlType.Npc && other.CompareTag("Player"))
                _handler.OnCancelInteract();
        }
    }
}