using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using VendingMachineLib.Entities;

namespace VendingMachineLib.Tests
{
    public class OrderComparer : IEqualityComparer<Order>
    {
        public bool Equals([AllowNull] Order x, [AllowNull] Order y)
        {
            if (x == null)
                return false;
            if (y == null)
                return false;
            return x.OID.Equals(y.OID) && x.Amount.Equals(y.Amount) && x.Item.ID.Equals(y.Item.ID) && x.Quantity.Equals(y.Quantity);
        }

        public int GetHashCode([DisallowNull] Order obj)
        {
            return obj.ToString().GetHashCode();
        }
    }
}
