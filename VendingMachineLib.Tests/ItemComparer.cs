using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using VendingMachineLib.Entities;

namespace VendingMachineLib.Tests
{
    public class ItemComparer : IEqualityComparer<Item>
    {
        public bool Equals([AllowNull] Item x, [AllowNull] Item y)
        {
            if (x == null)
                return false;
            if (y == null)
                return false;
            return x.ID.Equals(y.ID) && x.Name.Equals(y.Name) && x.Price.Equals(y.Price) && x.Quantity.Equals(y.Quantity);
        }

        public int GetHashCode([DisallowNull] Item obj)
        {
            return obj.GetHashCode();
        }
    }
}
