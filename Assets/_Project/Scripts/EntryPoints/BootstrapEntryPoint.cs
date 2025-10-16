using _Project.Scripts.Loading;
using _Project.Scripts.Services;
using UnityEngine;
using System.Collections.Generic;

namespace _Project.Scripts.EntryPoints
{
    public class BootstrapEntryPoint : MonoBehaviour
    {
        private async void Start()
        {
            var fakeService1 = new FakeService();
            var fakeService2 = new FakeService();
            var fakeService3 = new FakeService();

            var loadingSteps = new List<LoadingStep>
            {
                new("Load Fake Service 1..", fakeService1.InitializeAsync),
                new("Initialize Fake Service 2..", fakeService2.InitializeAsync),
                new("Load Fake Service 3..", fakeService3.InitializeAsync),
            };

            await LoadingManager.Instance.LoadSceneAsync("Game", loadingSteps);
        }
    }
}