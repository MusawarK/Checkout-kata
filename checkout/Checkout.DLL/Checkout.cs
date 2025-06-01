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

        private readonly Dictionary<string, int> _prices = new()
        {
            { "A", 50 },
            { "B", 30 },
            { "C", 20 },
            { "D", 15 }
        };

        public Checkout() {
            _items = new Dictionary<string, int>();
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
                if (_prices.ContainsKey(item.Key))
                    total += _prices[item.Key];
            } 
            
            return total;
        }
    }
}
