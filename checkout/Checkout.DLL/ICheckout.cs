using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkout.DLL
{
    interface ICheckout
    {
        void Scan(string item);
        int GetTotalPrice();
    }
}
