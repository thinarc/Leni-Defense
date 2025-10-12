using UnityEngine;

namespace _Project.Scripts.Wallet
{
    public class WalletPhysics : MonoBehaviour
    {
        [SerializeField] private GameObject coinPrefab;
        [SerializeField] private GameObject rubyPrefab;
        
        [Space(5)]
        [SerializeField] private Vector2 spawnPoint;
        
        public void RegisterResource(ResourceType resourceType)
        {
            var prefab = resourceType == ResourceType.Coin ? coinPrefab : rubyPrefab;
            var res = Instantiate(prefab, spawnPoint + (Vector2)transform.position, Quaternion.identity);
            res.transform.SetParent(transform);
            res.transform.localScale = prefab.transform.localScale;
        }

        public bool TryFindResource(ResourceType resourceType)
        {
            return false;
        }

        private void ThrowResource(GameObject res)
        {
            Destroy(res);
        }
        private void OnTriggerExit2D(Collider2D other) { if (other.CompareTag("Resource")) ThrowResource(other.gameObject); }


        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(spawnPoint + (Vector2)transform.position, 0.06f);
        }
    }
}