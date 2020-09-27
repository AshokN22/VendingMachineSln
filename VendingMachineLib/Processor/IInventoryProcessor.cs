using System;
using System.Collections.Generic;
using System.Text;
using VendingMachineLib.Entities;

namespace VendingMachineLib.Processor
{
    public interface IInventoryProcessor
    {
        IOrderProcessor OrderProcessor { get; set; }
        Dictionary<string, Item> GetItems();
    }
}
