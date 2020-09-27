using System;
using System.Collections.Generic;
using System.Text;
using VendingMachineLib.Entities;

namespace VendingMachineLib.Processor
{
    public class InventoryProcessor : IInventoryProcessor
    {
        private IOrderProcessor orderProcessor = null;

        public InventoryProcessor(IOrderProcessor ordProcessor)
        {
            orderProcessor = ordProcessor;
        }

        public Dictionary<string, Item> GetItems()
        {
            throw new NotImplementedException();
        }
    }
}
