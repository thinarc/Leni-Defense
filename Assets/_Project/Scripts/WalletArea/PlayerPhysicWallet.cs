using UnityEngine;
using UnityEngine.Serialization;

namespace _Project.Scripts.WalletArea
{
    public class PlayerPhysicWallet : MonoBehaviour
    {
        [SerializeField, Space(5)] private PlayerWallet wallet;
        
        [FormerlySerializedAs("coinPrefab")] [SerializeField] private GameObject uiCoinPrefab;
        [FormerlySerializedAs("rubyPrefab")] [SerializeField] private GameObject uiRubyPrefab;
        
        [SerializeField, Space(5)] private Vector2 spawnPoint;
        [FormerlySerializedAs("coinLayer")] [SerializeField, Space(5)] private LayerMask uiCoinLayer;
        [FormerlySerializedAs("rubyLayer")] [SerializeField] private LayerMask uiRubyLayer;
        
        public void RegisterResource(ResourceType resourceType)
        {
            var prefab = resourceType == ResourceType.Coin ? uiCoinPrefab : uiRubyPrefab;
            var res = Instantiate(prefab, spawnPoint + (Vector2)transform.position, Quaternion.identity);
            res.transform.SetParent(transform);
            res.transform.localScale = prefab.transform.localScale;
            var pos = res.transform.localPosition;
            pos.z = 0;
            res.transform.localPosition = pos;
        }
        
        public bool TryFindByResource(ResourceType resourceType)
        {
            LayerMask mask = resourceType == ResourceType.Coin ? uiCoinLayer : uiRubyLayer;
            return TryFindResource(mask, out _);
        }
        
        public bool TryFindAnyResource(out ResourceType? resourceType)
        {
            LayerMask mask = uiCoinLayer | uiRubyLayer;
            return TryFindResource(mask, out resourceType);
        }

        private bool TryFindResource(LayerMask targetMask, out ResourceType? foundResourceType)
        {
            RaycastHit2D hit = Physics2D.CapsuleCast(
                origin: spawnPoint + (Vector2)transform.position,
                size: new Vector2(1f, 1f),
                capsuleDirection: CapsuleDirection2D.Vertical,
                angle: 0f,
                direction: Vector2.down,
                distance: Mathf.Infinity,
                layerMask: targetMask
            );

            if (hit.collider)
            {
                foundResourceType = hit.collider.CompareTag("CoinUI") ? ResourceType.Coin : ResourceType.Ruby;
                DisposeUiResource(hit.collider.gameObject);
                return true;
            }

            foundResourceType = null;
            return false;
        }

        private void DisposeUiResource(GameObject res) => Destroy(res);
        // private void ThrowUiResource(GameObject res) => wallet.ConcreteSpawnResource(res.CompareTag("CoinUI") ? ResourceType.Coin : ResourceType.Ruby);
        private void LoseResource(GameObject res)
        {
            DisposeUiResource(res);
            wallet.ManualSpendResource(res.CompareTag("CoinUI") ? ResourceType.Coin : ResourceType.Ruby);
        }
        // Проблема в том что он срабатывает даже когда мы утилизируем ресурс посредством TryFindResource, а не
        // когда ресурс действительно вылетает из кошелька
        private void OnTriggerExit2D(Collider2D other) { if (other.CompareTag("CoinUI") || other.CompareTag("RubyUI")) LoseResource(other.gameObject); }


        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(spawnPoint + (Vector2)transform.position, 0.06f);
        }
    }
}