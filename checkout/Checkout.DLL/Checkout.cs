using Checkout.DLL.Strategy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkout.DLL
{
    public class Checkout : ICheckout
    {
        private readonly Dictionary<string, int> _items;

        private readonly IPricingStrategy _pricingStrategy;

        public Checkout(IPricingStrategy pricingStrategy) {
            _items = new Dictionary<string, int>();
            _pricingStrategy = pricingStrategy;
        }

        public void Scan(string item)
        {
            if (_items.ContainsKey(item))
                _items[item]++;
            else
                _items[item] = 1;
        }

        public int GetTotalPrice()
        {
            int total = 0;

            if (!_items.Any())
                total = 0;

            foreach (var item in _items)
            {
                total += _pricingStrategy.CalculateTotalPrice(item.Key, item.Value);
            }

            return total;
        }
    }
}
