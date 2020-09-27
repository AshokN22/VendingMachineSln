using System;
using System.Collections.Generic;
using System.Text;
using VendingMachineLib.Entities;

namespace VendingMachineLib.Processor
{
    public class OrderProcessor : IOrderProcessor
    {
        private IInventoryProcessor inventoryProcessor = null;

        public OrderProcessor(IInventoryProcessor invProcessor)
        {
            inventoryProcessor = invProcessor;
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
