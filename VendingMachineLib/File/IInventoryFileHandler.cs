using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VendingMachineLib.Entities;

namespace VendingMachineLib.File
{
    public interface IInventoryFileHandler
    {
        Task<Dictionary<string, Item>> FetchItems();
    }
}
