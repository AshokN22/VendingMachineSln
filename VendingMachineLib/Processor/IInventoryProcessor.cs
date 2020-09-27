using System;
using System.Collections.Generic;
using System.Text;
using VendingMachineLib.Entities;

namespace VendingMachineLib.Processor
{
    public interface IInventoryProcessor
    {
        Dictionary<string, Item> GetItems();
    }
}
