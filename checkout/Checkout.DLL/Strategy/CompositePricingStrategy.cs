using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkout.DLL.Strategy
{
    public class CompositePricingStrategy : IPricingStrategy
    {
        private readonly List<IPricingStrategy> _pricingStrategies;

        public CompositePricingStrategy(List<IPricingStrategy> pricingStrategies)
        {
            _pricingStrategies = pricingStrategies;
        }

        public int CalculateTotalPrice(string item, int quantity)
        {
            return _pricingStrategies
                .Select(s => s.CalculateTotalPrice(item, quantity))
                .FirstOrDefault(price => price > 0);
        }
    }
}
