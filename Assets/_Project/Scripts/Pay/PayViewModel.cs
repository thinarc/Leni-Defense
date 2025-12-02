using System;

namespace _Project.Scripts.Pay
{
    public class PayViewModel
    {
        private PayModel _model;

        public PayViewModel(PayModel model) => UpdateModel(model);

        public void UpdateModel(PayModel model) => _model = model;

        public event Action OnCompletePay;

        public bool TryPaySlot()
        {
            if (_model.Price <= 0 || _model.AnimatedPrice <= 0) return false;
            
            _model.AnimatedPrice--;
            if (_model.AnimatedPrice == 0)
                OnCompletePay?.Invoke();
            
            return true;
        }
    }
}