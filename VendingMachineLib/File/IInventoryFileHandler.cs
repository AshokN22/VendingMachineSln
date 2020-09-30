using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VendingMachineLib.Entities;

namespace VendingMachineLib.File
{
    public interface IInventoryFileHandler
    {
        string ItemCSVPath { get; set; }
        Task<Dictionary<string, Item>> FetchItems();
    }
}
