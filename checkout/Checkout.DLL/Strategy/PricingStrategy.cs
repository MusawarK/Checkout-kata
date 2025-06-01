using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkout.DLL.Strategy
{
    public class PricingStrategy : IPricingStrategy
    {

        private readonly Dictionary<string, int> _prices = new()
        {
            { "A", 50 },
            { "B", 30 },
            { "C", 20 },
            { "D", 15 }
        };

        public int CalculateTotalPrice(string item, int quantity)
        {
            if (item == "A")
            {
                int setsOfMultipleItems = quantity / 3;
                int remainingItems = quantity % 3;
                return (setsOfMultipleItems * 130) + (remainingItems * 50);
            }

            return _prices.TryGetValue(item, out var price) ? quantity * price : 0;
        }
    }
}
