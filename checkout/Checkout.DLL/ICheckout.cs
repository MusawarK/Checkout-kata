using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkout.DLL
{
    public interface ICheckout
    {
        void Scan(string item);

        // *********************************************************************************************************************************************
        // IMPORTANT NOTES
        // The requirements specified the use of int for pricing, so it has been retained as-is.
        // However, for these type of values, the recommended data type is decimal due to its higher precision and accuracy in financial calculations.
        // *********************************************************************************************************************************************
        int GetTotalPrice(); 
    }
}
