using UnityEngine;
using System.Collections.Generic;
using _Project.Scripts.Input;
using _Project.Scripts.Loading;
using _Project.Scripts.Services;

namespace _Project.Scripts.EntryPoints
{
    public class BootstrapEntryPoint : MonoBehaviour
    {
        [SerializeField] private InputManager inputManager;
        [SerializeField] private LoadingManager loadingManager;
        [SerializeField] private Transform eventSystem;
        
        private async void Start()
        {
            inputManager.Initialize();
            loadingManager.Initialize();
            DontDestroyOnLoad(eventSystem);
            
            var fakeService = new FakeService();

            var loadingSteps = new List<LoadingStep>
            {
                new("Load Fake Service..", fakeService.InitializeAsync),
            };

            await LoadingManager.Instance.LoadSceneAsync("Game", loadingSteps);
        }
    }
}