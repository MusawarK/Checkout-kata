using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkout.DLL.Strategy
{
    public class BulkPricingStrategy : IPricingStrategy
    {
        private readonly string _item;
        private readonly int _price;
        private readonly int _bulkItemsQuantity;
        private readonly int _bulkPrice;

        public BulkPricingStrategy(string item, int price, int bulkQuantity, int bulkPrice)
        {
            _item = item;
            _price = price;
            _bulkItemsQuantity = bulkQuantity;
            _bulkPrice = bulkPrice;
        }

        public int CalculateTotalPrice(string item, int quantity)
        {
            if (item != _item || quantity <= 0)
                return 0;

            int setsOfMultipleItems = quantity / _bulkItemsQuantity;
            int remainingItems = quantity % _bulkItemsQuantity;

            return (setsOfMultipleItems * _bulkPrice) + (remainingItems * _price);
        }
    }
}
