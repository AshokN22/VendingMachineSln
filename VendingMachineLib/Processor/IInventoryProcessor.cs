using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VendingMachineLib.Entities;

namespace VendingMachineLib.Processor
{
    public interface IInventoryProcessor
    {
        IOrderProcessor OrderProdcessor { get; set; }
        Task<Dictionary<string, Item>> GetItems();
    }
}
