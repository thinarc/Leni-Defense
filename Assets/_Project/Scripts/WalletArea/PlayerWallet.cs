using _Project.Scripts.Movement;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Sirenix.OdinInspector;
using Zenject;

namespace _Project.Scripts.WalletArea
{
    public class PlayerWallet : MonoBehaviour, IWallet, IInitializable
    {
        [SerializeField] private PlayerPhysicWallet physicWallet;
        
        [SerializeField, Space(5)] private Vector2 throwOffset;
        [SerializeField] private Transform parentThrow;
        [SerializeField, Header("Debug")] private MovementController player;
        
        private float Flip => player.transform.localScale.x >= 0 ? 1 : -1;
        public static Vector2 ThrowOffset;
        public static Transform ParentThrow;

        [Inject]
        public void Construct(List<MovementController> movementControllers) => player = movementControllers[0];
        
        private int Coins { get; set; }
        private int Rubies { get; set; }

        public void Initialize()
        {
            ThrowOffset = throwOffset;
            ParentThrow = parentThrow;
        }

        public bool AddCurrency(int lootType)
        {
            if (lootType == 0) Coins++;
            else Rubies++;
            physicWallet.RegisterCurrency(lootType);
            return true;
        }

        public bool ThrowCurrency()
        {
            if (!physicWallet.TryFindAnyCurrency(out var foundType)) return false;
            if (foundType == 0) Coins--;
            else Rubies--;
            SpawnLoot(foundType, (Vector2)player.transform.position + new Vector2(throwOffset.x * Flip, throwOffset.y), parentThrow, Flip);
            return true;
        }
        
        public bool SpendCurrency(int currencyType)
        {
            if (!physicWallet.TryFindByCurrency(currencyType)) return false;
            if (currencyType == 0) Coins--;
            else Rubies--;
            return true;
        }
        public void ManualSpendCurrency(int currencyType)
        {
            if (currencyType == 0) Coins--;
            else Rubies--;
        }
        
        public static void SpawnLoot(int? lootType, Vector2 spawnPos, Transform spawnParent, float flip = 1)
        {
            if (lootType == null) return;
            var prefab = Resources.Load<GameObject>(lootType == 0 ? "Coin" : "Ruby");
            var loot = Instantiate(prefab, spawnPos, Quaternion.identity).transform;
            loot.SetParent(spawnParent);
            var randVariation = 1 + Random.Range(-0.31f, 0.31f);
            loot.DOMove((Vector2)loot.position + new Vector2(1 * flip, 0.7f) * randVariation, 0.25f).SetEase(Ease.OutQuad);
        }
        
        [Button, Title("Debug"), PropertySpace(5)] private void AddCoin() => AddCurrency(0);
        [Button] private void AddRuby() => AddCurrency(1);
        [Button] private void SpendCoin() => SpendCurrency(0);
        [Button] private void SpendRuby() => SpendCurrency(1);
        [Button] private void ThrowAny() => ThrowCurrency();
    }
}