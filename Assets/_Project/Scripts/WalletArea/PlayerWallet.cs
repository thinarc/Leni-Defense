using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.Serialization;
using DG.Tweening;

namespace _Project.Scripts.WalletArea
{
    public class PlayerWallet : MonoBehaviour, IWallet
    {
        [FormerlySerializedAs("physics")] [SerializeField] private PlayerPhysicWallet physicWallet;
        
        [SerializeField, Space(5)] private Transform player;
        [SerializeField] private Vector2 spawnOffset;
        [SerializeField] private Transform parentSpawn;
        
        public int Coins { get; private set; }
        public int Rubies { get; private set; }
        
        public static Vector2 SpawnOffset;
        public static Transform ParentSpawn;
        private void Awake()
        {
            SpawnOffset = spawnOffset;
            ParentSpawn = parentSpawn;
        }
        private void OnDisable()
        {
            SpawnOffset = Vector2.zero;
            ParentSpawn = null;
        }
        
        public bool AddResource(ResourceType resourceType)
        {
            if (resourceType == ResourceType.Coin) Coins++;
            else Rubies++;
            physicWallet.RegisterResource(resourceType);
            return true;
        }

        public bool ThrowResource()
        {
            if (!physicWallet.TryFindAnyResource(out ResourceType? foundType)) return false;
            if (foundType == ResourceType.Coin) Coins--;
            else if (foundType == ResourceType.Ruby) Rubies--;
            float flip = player.localScale.x >= 0 ? 1 : -1;
            SpawnResource(foundType, (Vector2)player.position + new Vector2(spawnOffset.x * flip, spawnOffset.y), parentSpawn, flip);
            return true;
        }
        public float Flip => player.localScale.x >= 0 ? 1 : -1;
        public void ConcreteSpawnResource(ResourceType resourceType) => SpawnResource(resourceType, (Vector2)player.position + new Vector2(spawnOffset.x * Flip, spawnOffset.y), parentSpawn, Flip);
        
        public bool SpendResource(ResourceType resourceType)
        {
            if (!physicWallet.TryFindByResource(resourceType)) return false;
            if (resourceType == ResourceType.Coin) Coins--;
            else Rubies--;
            return true;
        }
        public void ManualSpendResource(ResourceType resourceType)
        {
            if (resourceType == ResourceType.Coin) Coins--;
            else Rubies--;
        }
        
        public static void SpawnResource(ResourceType? resourceType, Vector2 spawnPos, Transform parent, float flip = 1)
        {
            if (resourceType == null) return;
            var prefab = Resources.Load<GameObject>(resourceType.Value.ToString());
            var res = Instantiate(prefab, spawnPos, Quaternion.identity).transform;
            res.SetParent(parent);
            float randVariation = 1 + Random.Range(-0.3f, 0.3f);
            res.DOMove((Vector2)res.position + new Vector2(1 * flip, 0.7f) * randVariation, 0.23f * randVariation).SetEase(Ease.OutQuad);
        }
        
        [Button, Title("Debug"), PropertySpace(3)] private void AddCoin() => AddResource(ResourceType.Coin);
        [Button] private void AddRuby() => AddResource(ResourceType.Ruby);
        [Button] private void SpendCoin() => SpendResource(ResourceType.Coin);
        [Button] private void SpendRuby() => SpendResource(ResourceType.Ruby);
        [Button] private void ThrowAny() => ThrowResource();
    }
}