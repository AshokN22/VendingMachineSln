using System;
using System.Collections.Generic;
using System.Text;
using VendingMachineLib.Entities;

namespace VendingMachineLib.Processor
{
    public interface IOrderProcessor
    {
        Dictionary<string, Order> GetOrders();
        void SaveOrder(Order order);
    }
}
