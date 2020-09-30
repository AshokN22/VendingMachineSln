using System;
using System.Collections.Generic;
using System.Text;

namespace VendingMachineLib.Entities
{
    public class Order
    {
        public int OID { get; set; }
        public float Amount { get; set; }
        public Item Item { get; set; }
        public int Quantity { get; set; }

        public override string ToString()
        {
            return $"{OID},{Amount},{Item.ToString()},{Quantity}";
        }
    }
}
