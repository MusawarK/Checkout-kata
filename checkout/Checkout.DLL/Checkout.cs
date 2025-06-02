using Checkout.DLL.Strategy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkout.DLL
{
    // **********************************************************************************************************************************
    // IMPORTANT NOTES
    // Future Enhancements
    // The current implementation fulfills the requirements of the kata.
    // A repository can be introduced later, depending on where the pricing data will be fetched from (e.g., in-memory, database, API).
    // A factory may be added to write the logic for selecting the appropriate repository (e.g., mock for testing, actual for runtime).
    // **********************************************************************************************************************************

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
                var itemTotal = _pricingStrategy.CalculateTotalPrice(item.Key, item.Value);
                if(itemTotal <= 0)
                {
                    throw new InvalidOperationException($"Unknown item '{item.Key}' in the basket.");
                }
                total += itemTotal;
            }

            return total;
        }
    }
}
