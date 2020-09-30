using System;
using System.Collections.Generic;
using System.Text;

namespace VendingMachineLib.Entities
{
    public class Item
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public float Price { get; set; }

        public override string ToString()
        {
            return $"{ID},{Name},{Quantity},{Price}";
        }
    }
}
