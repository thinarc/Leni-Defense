using Cysharp.Threading.Tasks;

namespace _Project.Scripts.Services
{
    public class FakeService
    {
        public UniTask InitializeAsync()
        {
            return UniTask.Delay(0);
        }
    }
}