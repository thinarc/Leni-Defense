using UnityEngine;
using System.Collections.Generic;

namespace _Project.Scripts.WalletArea
{
    public class PlayerPhysicWallet : MonoBehaviour
    {
        [SerializeField, Space(5)] private PlayerWallet wallet;
        
        [SerializeField, Space(5)] private GameObject coinUiPrefab;
        [SerializeField] private GameObject rubyUiPrefab;
        
        [SerializeField, Space(5)] private Vector2 spawnPoint;
        [SerializeField, Space(5)] private LayerMask coinUiLayer;
        [SerializeField] private LayerMask rubyUiLayer;

        private readonly List<GameObject> _registeredCurrency = new();
        
        public void RegisterCurrency(int currencyType)
        {
            var prefab = currencyType == 0 ? coinUiPrefab : rubyUiPrefab;
            var randVariation = 1 + Random.Range(-0.1f, 0.1f);
            spawnPoint.y *= randVariation;
            var currency = Instantiate(prefab, (Vector2)transform.position + spawnPoint, Quaternion.identity);
            currency.transform.SetParent(transform);
            currency.transform.localScale = prefab.transform.localScale;
            var pos = currency.transform.localPosition;
            pos.z = 0;
            currency.transform.localPosition = pos;
            _registeredCurrency.Add(currency);
        }
        
        public bool TryFindByCurrency(int currencyType)
        {
            var mask = currencyType == 0 ? coinUiLayer : rubyUiLayer;
            return TryFindCurrency(mask, out _);
        }
        
        public bool TryFindAnyCurrency(out int? currencyType)
        {
            var mask = coinUiLayer | rubyUiLayer;
            return TryFindCurrency(mask, out currencyType);
        }

        private bool TryFindCurrency(LayerMask targetMask, out int? foundType)
        {
            var hit = Physics2D.CapsuleCast(
                origin: (Vector2)transform.position + spawnPoint,
                size: new Vector2(1f, 1f),
                capsuleDirection: CapsuleDirection2D.Vertical,
                angle: 0f,
                direction: Vector2.down,
                distance: Mathf.Infinity,
                layerMask: targetMask
            );

            if (hit.collider)
            {
                foundType = hit.collider.CompareTag("CoinUI") ? 0 : 1;
                DestroyUiObject(hit.collider.gameObject);
                return true;
            }

            foundType = null;
            return false;
        }

        private static void DestroyUiObject(GameObject obj) => Destroy(obj);
        private void OnTriggerExit2D(Collider2D other) => LoseCurrency(other.gameObject);
        private void LoseCurrency(GameObject currency)
        {
            if (!currency.CompareTag("CoinUI") && !currency.CompareTag("RubyUI")) return;
            if (!_registeredCurrency.Contains(currency)) return;
            _registeredCurrency.Remove(currency.gameObject);
            DestroyUiObject(currency);
            wallet.ManualSpendCurrency(currency.CompareTag("CoinUI") ? 0 : 1);
        }

        
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere((Vector2)transform.position + spawnPoint, 0.076f);
        }
    }
}