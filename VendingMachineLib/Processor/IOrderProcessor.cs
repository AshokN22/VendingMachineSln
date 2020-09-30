using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VendingMachineLib.Entities;

namespace VendingMachineLib.Processor
{
    public interface IOrderProcessor
    {
        IInventoryProcessor InventoryProcessor { get; set; }
        Task<Dictionary<string, Order>> GetOrders();
        Task<string> SaveOrder(Order order);
    }
}
