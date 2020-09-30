using System;
using VendingMachineLib.File;
using VendingMachineLib.Processor;

namespace inv
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                FileHandler fh = new FileHandler();
                fh.ItemCSVPath = @"D:\Upwork\Assignment\VendingMachineSln\inventory.csv";
                fh.OrderCSVPath = @"D:\Upwork\Assignment\VendingMachineSln\orders.csv";
                var invproc = new InventoryProcessor(fh);
                invproc.OrderProdcessor = new OrderProcessor(fh) { InventoryProcessor = invproc };
                var items = invproc.GetItems().Result;
                foreach (var item in items.Values)
                {
                    Console.WriteLine($"{item.ID}.{item.Name}({item.Quantity}): {item.Price.ToString("c")}");
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
