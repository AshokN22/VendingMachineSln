using System;
using System.Collections.Generic;
using System.Text;
using VendingMachineLib.Entities;

namespace VendingMachineLib.File
{
    public interface IInventoryFileHandler
    {
        Dictionary<string, Item> FetchItems();
    }
}
