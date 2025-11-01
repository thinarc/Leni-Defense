using _Project.Scripts.Abilities;
using _Project.Scripts.Brains;
using _Project.Scripts.Input;
using _Project.Scripts.Movement;
using _Project.Scripts.WalletArea;
using UnityEngine;
using System.Linq;

namespace _Project.Scripts.EntryPoints
{
    public class GameEntryPoint : MonoBehaviour
    {
        [SerializeField] private Transform eventSystem;
        [SerializeField] private Object canvas;
        
        [SerializeField] private InputManager inputManager;
        [SerializeField] private PlayerBrain  playerBrain;
        [SerializeField] private MovementController  movementController;
        [SerializeField] private PlayerWallet playerWallet;
        [SerializeField] private InteractLootAbility playerInteractLootAbility;
        
        private void Start()
        {
            if (eventSystem.parent) eventSystem.SetParent(null);
            DontDestroyOnLoad(eventSystem);
            DontDestroyOnLoad(canvas);
            
            inputManager.Initialize();
            playerBrain.Initialize();
            movementController.Initialize();
            playerWallet.Initialize();
            playerInteractLootAbility.Initialize();
            var interactsLoot = FindObjectsByType<InteractLootAbility>(FindObjectsSortMode.InstanceID)
                .Where(item => item != playerInteractLootAbility);
            foreach (var interact in interactsLoot) interact.Initialize();
        }
    }
}