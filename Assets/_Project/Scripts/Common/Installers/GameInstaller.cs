using _Project.Scripts.Abilities.InteractLoot;
using _Project.Scripts.Brains;
using _Project.Scripts.Movement;
using _Project.Scripts.WalletArea;
using _Project.Scripts.Common.EntryPoints;
using Unity.Cinemachine;
using UnityEngine;
using System.Linq;
using _Project.Scripts.Pay;
using Zenject;

namespace _Project.Scripts.Common.Installers
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private MovementController playerPrefab;
        [SerializeField] private Transform playerSpawnPoint;
        [SerializeField] private PlayerWallet playerWallet;
        
        [SerializeField] private MovementController npcMovement;
        [SerializeField] private PayView basePayView;
        [SerializeField] private CinemachineCamera vCam;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<PlayerMoveBrain>().AsSingle().NonLazy();
            Container.Bind<MoveBrain>().To<PlayerMoveBrain>().FromResolve();
            var player = Container.InstantiatePrefab(playerPrefab, playerSpawnPoint.position, Quaternion.identity, null);
            var playerInteractLootAbility = player.GetComponent<InteractLootAbility>();
            Container.BindInterfacesAndSelfTo<MovementController>().FromInstance(player.GetComponent<MovementController>()).AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<PlayerWallet>().FromInstance(playerWallet).AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<InteractLootAbility>().FromInstance(playerInteractLootAbility).AsSingle().NonLazy();
            
            var interactsLoot = FindObjectsByType<InteractLootAbility>(FindObjectsSortMode.InstanceID)
                .Where(item => item != playerInteractLootAbility);
            interactsLoot.ToList().ForEach(item => Container.BindInterfacesAndSelfTo<InteractLootAbility>().FromInstance(item).AsTransient().NonLazy());
            
            Container.BindInterfacesAndSelfTo<NpcMoveBrain>().AsSingle().NonLazy();
            Container.Rebind<MoveBrain>().To<NpcMoveBrain>().FromResolve();
            Container.BindInterfacesAndSelfTo<MovementController>().FromInstance(npcMovement).AsTransient().NonLazy();

            Container.Bind<int>().FromInstance(basePayView.Price).AsSingle().NonLazy();
            Container.Bind<PayModel>().AsSingle().NonLazy();
            Container.Bind<PayViewModel>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<PayView>().FromInstance(basePayView).AsSingle().NonLazy();
            
            Container.Bind<CinemachineCamera>().FromInstance(vCam).AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<GameEntryPoint>().AsSingle().NonLazy();
        }
    }
}