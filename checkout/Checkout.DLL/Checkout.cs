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

        public Checkout() {
            _items = new Dictionary<string, int>();
        }

        public int GetTotalPrice()
        {
            return 0;
        }

    }
}
