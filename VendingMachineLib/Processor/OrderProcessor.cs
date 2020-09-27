using System;
using System.Collections.Generic;
using System.Text;
using VendingMachineLib.Entities;

namespace VendingMachineLib.Processor
{
    public class OrderProcessor : IOrderProcessor
    {
        private IInventoryProcessor InventoryProcessor = null;

        public OrderProcessor(IInventoryProcessor invProcessor)
        {
            InventoryProcessor = invProcessor;
        }

        public Dictionary<string, Order> GetOrders()
        {
            throw new NotImplementedException();
        }

        public void SaveOrder(Order order)
        {
            throw new NotImplementedException();
        }
    }
}
