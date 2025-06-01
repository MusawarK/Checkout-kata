using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkout.DLL.Strategy
{
    public class PricingStrategy : IPricingStrategy
    {
        private readonly string _item;
        private readonly int _price;

        public PricingStrategy(string item, int price)
        {
            _item = item;
            _price = price;
        }

        public int CalculateTotalPrice(string item, int quantity)
        {
            return item == _item ? quantity * _price : 0;
        }
    }
}
