using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkout.DLL.Strategy
{
    public interface IPricingStrategy
    {
        // *********************************************************************************************************************************************
        // IMPORTANT NOTES
        // The requirements specified the use of int for pricing, so it has been retained as-is.
        // However, for these type of values, the recommended data type is decimal due to its higher precision and accuracy in financial calculations.
        // *********************************************************************************************************************************************
        int CalculateTotalPrice(string item, int quantity);
    }
}
