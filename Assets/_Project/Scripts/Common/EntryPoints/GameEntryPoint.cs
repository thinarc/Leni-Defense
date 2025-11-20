using _Project.Scripts.Movement;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Common.EntryPoints
{
    public class GameEntryPoint : IInitializable
    {
        [Inject] private readonly CinemachineCamera _camera;
        private readonly List<MovementController> _movementControllers;

        public GameEntryPoint(List<MovementController> movementControllers) => _movementControllers = movementControllers;

        public void Initialize()
        {
            _camera.Follow = _movementControllers[0].transform;
        }
    }
}