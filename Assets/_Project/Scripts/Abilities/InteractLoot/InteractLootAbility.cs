using _Project.Scripts.Abilities.InteractLoot.LootHandlers;
using _Project.Scripts.Input;
using _Project.Scripts.WalletArea;
using UnityEngine;
using Sirenix.OdinInspector;
using Zenject;

namespace _Project.Scripts.Abilities.InteractLoot
{
    public class InteractLootAbility : MonoBehaviour, IInitializable
    {
        private enum ControlType { Player, Npc, Enemy }
        [SerializeField] private ControlType controlType;
        
        private InputManager _input;
        private PlayerWallet _playerWallet;
        private float InteractDelay => controlType == ControlType.Player ? 0.27f : 0.6f;
        
        [ShowIf("controlType", ControlType.Npc), SerializeField, Space(5)] private Collider2D npcCollider;
        
        private IInteractLootAbility _handler;
        
        [Inject]
        private void Construct(InputManager input) => _input = input;

        public void Initialize()
        {
            _playerWallet = FindAnyObjectByType<PlayerWallet>();
            InitializeHandler();
        }
        [Button("Initialize manually"), PropertySpace(5)]
        private void InitializeHandler()
        {
            _handler = controlType switch
            {
                ControlType.Player => new PlayerInteractLootAbility(_playerWallet, _input, InteractDelay),
                ControlType.Npc => new NpcInteractLootAbility(transform, InteractDelay, npcCollider),
                ControlType.Enemy => new EnemyInteractLootAbility(transform),
                _ => null
            };
            
            if (_handler is IInitializable initializable)
                initializable.Initialize();
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