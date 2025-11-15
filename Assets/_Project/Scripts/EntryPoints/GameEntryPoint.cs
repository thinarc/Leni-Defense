using _Project.Scripts.Abilities;
using _Project.Scripts.Brains;
using _Project.Scripts.Movement;
using _Project.Scripts.WalletArea;
using UnityEngine;
using System.Linq;
using _Project.Scripts.Abilities.InteractLoot;

namespace _Project.Scripts.EntryPoints
{
    public class GameEntryPoint : MonoBehaviour
    {
        [SerializeField] private MovementController playerMovement;
        [SerializeField] private PlayerWallet playerWallet;
        [SerializeField] private InteractLootAbility playerInteractLootAbility;

        [SerializeField] private MovementController npcMovement;
        
        private void Start()
        {
            playerMovement.Initialize(new PlayerMoveBrain().Initialize());
            playerWallet.Initialize();
            playerInteractLootAbility.Initialize();
            var interactsLoot = FindObjectsByType<InteractLootAbility>(FindObjectsSortMode.InstanceID)
                .Where(item => item != playerInteractLootAbility);
            foreach (var interact in interactsLoot) interact.Initialize();
            
            npcMovement.Initialize(new NpcMoveBrain());
        }
    }
}