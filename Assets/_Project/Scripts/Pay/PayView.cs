using UnityEngine;
using Zenject;
using System.Collections.Generic;
using _Project.Scripts.Common;
using DG.Tweening;
using Cysharp.Threading.Tasks;

namespace _Project.Scripts.Pay
{
    public class PayView : MonoBehaviour, IInitializable
    {
        [SerializeField] private int price;
        [SerializeField] private int columns;
        
        public int Price => price;
        
        private PayViewModel _viewModel;
        
        private CanvasGroup _canvasGroup;
        private List<List<GameObject>> _slotsGroups;
        
        private bool inPayArea;

        [Inject]
        private void Construct(PayViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public void Initialize()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            
            _slotsGroups = new List<List<GameObject>>();
            var distribution = PeakDistribution.CalculateDistribution(price, columns);

            for (var i = 0; i < columns; i++)
            {
                var group = Instantiate(Resources.Load<GameObject>("Group"), transform).transform;
                _slotsGroups.Add(new List<GameObject>());

                var slotsAmount = distribution[i];
                for (var j = 0; j < slotsAmount; j++)
                {
                    var slot = Instantiate(Resources.Load<GameObject>("Slot"), group);
                    _slotsGroups[i].Add(slot);
                }
            }
        }

        public async void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag("Player")) return;
            await DoFade(1);
            inPayArea = true;
        }
        
        public async void OnTriggerExit2D(Collider2D other)
        {
            if (!other.CompareTag("Player")) return;
            await DoFade(0);
            inPayArea = false;
        }
        
        
        private async UniTask DoFade(float val) => await _canvasGroup.DOFade(val, FadeDuration).SetEase(EaseLine).AsyncWaitForCompletion();
        private const float FadeDuration = 0.4f;
        private const Ease EaseLine = Ease.InFlash;
    }
}