namespace _Project.Scripts.Pay
{
    public class PayModel
    {
        private int _animatedPrice;
        
        public PayModel(int price)
        {
            Price = price;
            AnimatedPrice = price;
        }

        public readonly int Price;

        public int AnimatedPrice
        {
            get => _animatedPrice;
            set => _animatedPrice = value < 0 ? 0 : value;
        }
    }
}