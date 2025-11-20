using _Project.Scripts.Services;
using _Project.Scripts.Loading;
using System.Collections.Generic;
using Zenject;

namespace _Project.Scripts.Common.EntryPoints
{
    public class BootstrapEntryPoint : IInitializable
    {
        private readonly LoadingManager _loading;
        
        public BootstrapEntryPoint(LoadingManager loading)
        {
            _loading = loading;
        }

        public async void Initialize()
        {
            var fakeService = new FakeService();

            var loadingSteps = new List<LoadingStep>
            {
                new("Load Fake Service..", fakeService.InitializeAsync),
            };

            await _loading.LoadSceneAsync("Game", loadingSteps);
        }
    }
}